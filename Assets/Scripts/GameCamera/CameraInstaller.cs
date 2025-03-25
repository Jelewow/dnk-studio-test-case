using Jelewow.DNK.Extensions;
using Jelewow.DNK.GameCamera.MonoBehaviours;
using Jelewow.DNK.GameCamera.ScriptableObjects;
using Jelewow.DNK.GameCamera.Services;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.GameCamera
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCamera _camera;
        [SerializeField] private PlayerCameraConfig _cameraConfig;

        public override void InstallBindings()
        {
            Container.BindScriptableObject(_cameraConfig);
            Container.BindObjectOnScene(_camera);
            Container.BindService<PlayerCameraService>();
        }
    }
}