using Jelewow.DNK.Extensions;
using Jelewow.DNK.Player.MonoBehaviours;
using Jelewow.DNK.Player.ScriptableObjects;
using Jelewow.DNK.Player.Services;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private PlayerInfoPopup _infoPopup;

        public override void InstallBindings()
        {
            Container.BindScriptableObject(_playerConfig);
            Container.BindObjectOnScene(_playerSpawnPoint);
            Container.BindObjectOnScene(_infoPopup);
            Container.BindPrefab(_playerView);
            
            Container.BindService<PlayerInstanceService>();
            Container.BindService<PlayerInputService>();
            Container.BindService<PlayerMovementService>();
            Container.BindService<PlayerAnimationService>();
            Container.BindService<PlayerCollectableService>();
        }
    }
}