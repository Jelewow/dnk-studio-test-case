using Jelewow.DNK.Extensions;
using Jelewow.DNK.UI.MonoBehaviours;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private VolumeSlider _volumeSlider;
        
        public override void InstallBindings()
        {
            Container.BindObjectOnScene(_volumeSlider);
        }
    }
}