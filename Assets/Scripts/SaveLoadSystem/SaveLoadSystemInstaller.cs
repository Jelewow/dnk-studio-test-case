using Jelewow.DNK.Extensions;
using Jelewow.DNK.SaveLoadSystem.Services;
using Zenject;

namespace Jelewow.DNK.SaveLoadSystem
{
    public class SaveLoadSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindService<PlayerPrefsSaveService>();
            Container.BindService<PlayerPrefsLoadService>();
        }
    }
}