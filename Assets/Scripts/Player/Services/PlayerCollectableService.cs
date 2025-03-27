using System.Collections.Generic;
using Jelewow.DNK.Farms.MonoBehaviours;
using Jelewow.DNK.Farms.ScriptableObjects;
using Jelewow.DNK.Farms.Services;
using Jelewow.DNK.Player.MonoBehaviours;
using Zenject;

namespace Jelewow.DNK.Player.Services
{
    public class PlayerCollectableService
    {
        [Inject] private readonly FarmResourceService _farmResourceService;
        [Inject] private readonly PlayerInfoPopup _infoPopup;

        private readonly Dictionary<Resource, int> _resources = new();

        public void Collect(FarmView farm)
        {
            var resource = _farmResourceService.Collect(farm);

            if (_resources.ContainsKey(resource.Resource))
            {
                _resources[resource.Resource] += resource.Size;
            }
            else
            {
                _resources.Add(resource.Resource, resource.Size);
            }
            
            _infoPopup.UpdateUI(in _resources);
        }
    }
}