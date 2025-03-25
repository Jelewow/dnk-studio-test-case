using Jelewow.DNK.Player.Animations;
using Jelewow.DNK.Player.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.Player.Services
{
    public class PlayerAnimationService : ITickable, IInitializable
    {
        [Inject] private readonly PlayerInstanceService _instanceService;

        private PlayerView _player;
        private Animator _animator;
        
        public void Initialize()
        {
            _player = _instanceService.PlayerViewInstance;
            _animator = _instanceService.PlayerViewInstance.Animator;
        }
        
        public void Tick()
        {
            var speed = _player.Speed;
            _animator.SetFloat(PlayerAnimations.Speed, speed);
        }
    }
}