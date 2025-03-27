using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Jelewow.DNK.Farms.MonoBehaviours;
using Jelewow.DNK.GameCamera.MonoBehaviours;
using Jelewow.DNK.Player.Types;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Jelewow.DNK.Player.Services
{
    public class PlayerMovementService : IInitializable, ITickable
    {
        [Inject] private readonly PlayerInstanceService _playerInstanceService;
        [Inject] private readonly PlayerCamera _playerCamera;
        [Inject] private readonly PlayerCollectableService _collectableService;
        [Inject] private readonly PlayerInputService _inputService;

        private NavMeshAgent _agent;

        private Vector2 _previousTapPosition;

        private readonly Dictionary<UniTask, CancellationTokenSource> _activeTasks = new();
        private readonly List<UniTask> _tasksToCancel = new();

        public void Initialize()
        {
            _agent = _playerInstanceService.PlayerViewInstance.NavMeshAgent;
        }

        public void Tick()
        {
            if (_inputService.InputData.TapPosition != _previousTapPosition)
            {
                MoveTo(_inputService.InputData);
            }

            foreach (var task in _activeTasks.Keys)
            {
                if (task.Status is UniTaskStatus.Canceled or UniTaskStatus.Succeeded)
                {
                    _tasksToCancel.Add(task);
                }
            }

            foreach (var activeTask in _tasksToCancel)
            {
                _activeTasks[activeTask].Dispose();
                _activeTasks.Remove(activeTask);
            }

            _tasksToCancel?.Clear();
        }

        private void MoveTo(PlayerInputData inputData)
        {
            var ray = _playerCamera.MainCamera.ScreenPointToRay(new Vector3(inputData.TapPosition.x, inputData.TapPosition.y, _playerCamera.MainCamera.transform.position.y));
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            foreach (var cts in _activeTasks.Values)
            {
                cts.Cancel();
            }

            if (hit.collider.TryGetComponent<FarmView>(out var farm))
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;

                var newTask = MoveToAsync(token, farm);

                _activeTasks.Add(newTask, cancellationTokenSource);
                return;
            }

            MoveTo(hit.point);
        }

        private void MoveTo(Vector3 target)
        {
            _previousTapPosition = _inputService.InputData.TapPosition;
            _agent.isStopped = false;
            _agent.SetDestination(target);
        }

        private async UniTask MoveToAsync(CancellationToken token, FarmView farmView)
        {
            MoveTo(farmView.transform.position);
            await WaitUntilAsync(() => !_agent.pathPending && _agent.remainingDistance < farmView.Scale / 2f + _agent.radius * 2f, token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            _agent.isStopped = true;
            _collectableService.Collect(farmView);
        }

        private async UniTask WaitUntilAsync(Func<bool> condition, CancellationToken token)
        {
            while (!condition())
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await UniTask.Yield();
            }
        }
    }
}