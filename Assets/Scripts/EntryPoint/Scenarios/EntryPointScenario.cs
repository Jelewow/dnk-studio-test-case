using Jelewow.DNK.Player.MonoBehaviours;
using Jelewow.DNK.Player.Services;
using Jelewow.DNK.SaveLoadSystem;
using Jelewow.DNK.UI.MonoBehaviours;
using Zenject;

namespace Jelewow.DNK.EntryPoint.Scenarios
{
    public class EntryPointScenario : IInitializable
    {
        [Inject] private readonly PlayerInstanceService _playerInstanceService;
        [Inject] private readonly PlayerSpawnPoint _playerSpawnPoint;
        [Inject] private readonly ILoadSystemService _loadSystemService;

        [Inject] private readonly VolumeSlider _volumeSlider;
        
        public void Initialize()
        {
            _playerInstanceService.CreatePlayer(_playerSpawnPoint.Position);
            
            var savedData = _loadSystemService.Load();
            _volumeSlider.LoadVolume(savedData);
        }
    }
}