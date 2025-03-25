using Jelewow.DNK.EntryPoint.Scenarios;
using Jelewow.DNK.Extensions;
using Zenject;

namespace Jelewow.DNK.EntryPoint
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindScenario<EntryPointScenario>();
        }
    }
}