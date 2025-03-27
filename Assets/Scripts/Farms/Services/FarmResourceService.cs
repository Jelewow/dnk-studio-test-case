using System.Linq;
using Jelewow.DNK.Farms.MonoBehaviours;
using Jelewow.DNK.Farms.Types;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.Farms.Services
{
    public class FarmResourceService : ITickable
    {
        [Inject] private readonly FarmContainer _farmContainer;

        private float _elapsedTime;
        
        public void Tick()
        {
            _elapsedTime += Time.deltaTime;
            
            foreach (var farm in _farmContainer.Farms)
            {
                var previousTick = Mathf.FloorToInt((_elapsedTime - Time.deltaTime) / farm.FarmConfig.TickRate);
                var currentTick = Mathf.FloorToInt(_elapsedTime / farm.FarmConfig.TickRate);

                var tickCount = currentTick - previousTick;

                for (int i = 0; i < tickCount; i++)
                {
                    farm.ProduceResource(farm.FarmConfig.ProducePerTick);
                }
            }
        }

        public ResourceCollectable Collect(FarmView farm)
        {
            var currentFarm = _farmContainer.Farms.First(f => f == farm);
            var resource = currentFarm.FarmConfig.Resource;
            var size = currentFarm.CollectResource();

            var resourceCollectable = new ResourceCollectable
            {
                Resource = resource,
                Size = size
            };

            return resourceCollectable;
        }
    }
}