using Jelewow.DNK.Extensions;
using Jelewow.DNK.Farms.MonoBehaviours;
using Jelewow.DNK.Farms.Services;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.Farms
{
    public class FarmInstaller : MonoInstaller
    {
        [SerializeField] private FarmContainer _farmContainer;
        
        public override void InstallBindings()
        {
            Container.BindObjectOnScene(_farmContainer);
            Container.BindService<FarmResourceService>();
        }
    }
}