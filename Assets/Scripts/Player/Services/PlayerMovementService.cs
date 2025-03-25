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

        private NavMeshAgent _agent;
        
        private readonly Dictionary<UniTask, CancellationTokenSource> _activeTasks = new();
        private readonly List<UniTask> _tasksToCancel = new();

        public void Initialize()
        {
            _agent = _playerInstanceService.PlayerViewInstance.NavMeshAgent;
        }
        
        public void Tick()
        {
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
        
        public void MoveTo(PlayerInputData inputData)
        {
            var ray = _playerCamera.MainCamera.ScreenPointToRay(inputData.MouseClickPosition);
            if(!Physics.Raycast(ray, out var hit))
            {
                return;
            }
            
            foreach (var cts in _activeTasks.Values)
            {
                cts.Cancel();
            }
            
            if (hit.collider.TryGetComponent<Farm>(out var farm))
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;
                
                var newTask = MoveToAsync(token, hit.point, farm);
                
                _activeTasks.Add(newTask, cancellationTokenSource);
                return;
            }

            MoveTo(hit.point);
        }

        private void MoveTo(Vector3 target)
        {
            _agent.isStopped = false;
            _agent.SetDestination(target);
        }
        
        private async UniTask MoveToAsync(CancellationToken token, Vector3 target, Farm farm)
        {
            MoveTo(target);
            await WaitUntilAsync(() => !_agent.pathPending && _agent.remainingDistance < farm.Scale / 2f, token);
            
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            _agent.isStopped = true;
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