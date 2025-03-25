using Jelewow.DNK.GameCamera.MonoBehaviours;
using Jelewow.DNK.GameCamera.ScriptableObjects;
using Jelewow.DNK.Player.Services;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.GameCamera.Services
{
    public class PlayerCameraService : ITickable
    {
        [Inject] private readonly PlayerCamera _camera;
        [Inject] private readonly PlayerCameraConfig _config;
        [Inject] private readonly PlayerInputService _inputService;
        
        public void Tick()
        {
            var nextPosition = _camera.transform.position;

            // if (Input.touchCount == 1)
            // {
            //     Touch touch = Input.GetTouch(0);
            //     if (touch.phase == TouchPhase.Began)
            //     {
            //         touchStart = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.y));
            //     }
            //     else if (touch.phase == TouchPhase.Moved)
            //     {
            //         Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.y));
            //         pos += direction;
            //     }
            // }
            
            var keysInput = new Vector3(_inputService.InputData.Horizontal, 0, _inputService.InputData.Vertical);
            
            var mouseLeft = _inputService.InputData.MousePosition.x >= Screen.width - _config.BorderThickness;
            var mouseRight = _inputService.InputData.MousePosition.x <= _config.BorderThickness;
            var mouseX = mouseLeft ? 1f : mouseRight ? -1f : 0f;
            
            var mouseUp = _inputService.InputData.MousePosition.y >= Screen.height - _config.BorderThickness;
            var mouseDown = _inputService.InputData.MousePosition.y <= _config.BorderThickness;
            var mouseZ = mouseUp ? 1f : mouseDown ? -1f : 0f;
            
            var mouseInput = new Vector3(mouseX, 0f, mouseZ);
            nextPosition += (mouseInput + keysInput) * _config.Speed * Time.deltaTime;
            
            nextPosition.x = Mathf.Clamp(nextPosition.x, -_config.Border.x, _config.Border.x);
            nextPosition.z = Mathf.Clamp(nextPosition.z, -_config.Border.y, _config.Border.y);

            _camera.transform.position = nextPosition;
        }
    }
}