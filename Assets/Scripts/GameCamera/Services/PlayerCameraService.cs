using Jelewow.DNK.Extensions;
using Jelewow.DNK.GameCamera.MonoBehaviours;
using Jelewow.DNK.GameCamera.ScriptableObjects;
using Jelewow.DNK.Player.Services;
using UnityEngine;
using Zenject;

namespace Jelewow.DNK.GameCamera.Services
{
    public class PlayerCameraService : ITickable, IInitializable
    {
        [Inject] private readonly PlayerCamera _camera;
        [Inject] private readonly PlayerCameraConfig _config;
        [Inject] private readonly PlayerInputService _inputService;

        private Camera _mainCamera;
        private Vector3 _tapStartWorldPoint;
        private Vector2 _cameraBorder;

        private bool _isUISelected;

        public void Initialize()
        {
            _mainCamera = _camera.MainCamera;
            AdjustBorder();
        }

        public void Tick()
        {
            var nextPosition = _camera.transform.position;

            if (Application.isMobilePlatform)
            {
                if (_inputService.InputData.TouchCount == 1)
                {
                    var touch = _inputService.InputData.Touch;

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            _isUISelected = touch.position.IsPointerOverUIObject();
                            _tapStartWorldPoint = _mainCamera.ScreenToWorldPoint(
                                new Vector3(_inputService.InputData.TapStartPosition.x, _inputService.InputData.TapStartPosition.y, _mainCamera.transform.position.y));
                            break;

                        case TouchPhase.Moved when !_isUISelected:
                            var move = _mainCamera.ScreenToWorldPoint(
                                new Vector3(_inputService.InputData.RawTapMovingPosition.x, _inputService.InputData.RawTapMovingPosition.y, _mainCamera.transform.position.y));
                            var direction = _tapStartWorldPoint - move;

                            nextPosition += direction * _config.Speed * Time.deltaTime;
                            break;

                        case TouchPhase.Ended or TouchPhase.Canceled:
                            _isUISelected = false;
                            break;
                    }
                }
            }
            else
            {
                var keysInput = new Vector3(_inputService.InputData.Horizontal, 0, _inputService.InputData.Vertical);

                var mouseLeft = _inputService.InputData.MousePosition.x >= Screen.width - _config.BorderThickness;
                var mouseRight = _inputService.InputData.MousePosition.x <= _config.BorderThickness;
                var mouseX = mouseLeft ? 1f : mouseRight ? -1f : 0f;

                var mouseUp = _inputService.InputData.MousePosition.y >= Screen.height - _config.BorderThickness;
                var mouseDown = _inputService.InputData.MousePosition.y <= _config.BorderThickness;
                var mouseZ = mouseUp ? 1f : mouseDown ? -1f : 0f;

                var mouseInput = new Vector3(mouseX, 0f, mouseZ);
                nextPosition += (mouseInput + keysInput) * _config.Speed * Time.deltaTime;
            }

            nextPosition.x = Mathf.Clamp(nextPosition.x, -_cameraBorder.x, _cameraBorder.x);
            nextPosition.z = Mathf.Clamp(nextPosition.z, -_cameraBorder.y, _cameraBorder.y);
            _camera.transform.position = nextPosition;
        }

        private void AdjustBorder()
        {
            _cameraBorder = _config.Border;
            var widthMultiplier = Screen.width / 1920f;
            var heightMultiplier = Screen.height / 1080f;

            _cameraBorder.x *= widthMultiplier;
            _cameraBorder.y *= heightMultiplier;
        }
    }
}