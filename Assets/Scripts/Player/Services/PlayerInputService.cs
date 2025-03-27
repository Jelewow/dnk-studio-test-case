using Jelewow.DNK.Extensions;
using Jelewow.DNK.Player.Types;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Input = UnityEngine.Input;

namespace Jelewow.DNK.Player.Services
{
    public class PlayerInputService : ITickable
    {
        private const float TouchPositionError = 400f;
        private const float TouchTimeError = 0.2f;
        
        private bool _isTapped;
        private bool _tapBegun;
        private Vector2 _tapBegunPosition;
        private Vector2 _cashedTapPosition;
        private PlayerInputData _inputData;
        private float _touchTime;

        public PlayerInputData InputData => _inputData;

        public void Tick()
        {
            _inputData = new PlayerInputData
            {
                MousePosition = Input.mousePosition,
                Vertical = Input.GetAxis("Vertical"),
                Horizontal = Input.GetAxis("Horizontal"),
                Touch = new Touch(),
                TapPosition = _cashedTapPosition
            };

            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.touchCount > 0)
            {
                HandleTouchInput();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                HandleMouseInput();
            }

            if (!_isTapped)
            {
                return;
            }

            _cashedTapPosition = _tapBegunPosition;
            _inputData.TapPosition = _tapBegunPosition;
            _isTapped = false;
        }

        private void HandleTouchInput()
        {
            var touch = Input.GetTouch(0);
            _inputData.TouchCount = Input.touchCount;
            _inputData.Touch = touch;

            switch (touch.phase)
            {
                case TouchPhase.Began when !touch.position.IsPointerOverUIObject():

                    _inputData.TapStartPosition = touch.position;
                    _tapBegunPosition = touch.position;
                    _touchTime = 0f;
                    break;

                case TouchPhase.Moved or TouchPhase.Stationary:
                    _inputData.RawTapMovingPosition = touch.position;
                    _touchTime += Time.deltaTime;
                    break;

                case TouchPhase.Ended:
                {
                    _touchTime += Time.deltaTime;

                    var actualError = (_tapBegunPosition - touch.position).sqrMagnitude;
                    var isOneTap = actualError < TouchPositionError && _touchTime < TouchTimeError;
                    if (!touch.position.IsPointerOverUIObject() && isOneTap)
                    {
                        _isTapped = true;
                    }
                    else
                    {
                        _isTapped = false;
                    }

                    break;
                }
            }
        }

        private void HandleMouseInput()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            _isTapped = true;
            _tapBegunPosition = Input.mousePosition;
        }
    }
}