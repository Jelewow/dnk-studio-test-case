using Jelewow.DNK.Player.Types;
using UnityEngine.EventSystems;
using Zenject;
using Input = UnityEngine.Input;

namespace Jelewow.DNK.Player.Services
{
    public class PlayerInputService : ITickable
    {
        [Inject] private readonly PlayerMovementService _playerMovementService;

        public PlayerInputData InputData { get; private set; }
        
        public void Tick()
        {
            InputData = new PlayerInputData
            {
                MousePosition = Input.mousePosition,
                Vertical = Input.GetAxis("Vertical"),
                Horizontal = Input.GetAxis("Horizontal")
            };
            
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            var inputData = new PlayerInputData
            {
                MouseClickPosition = Input.mousePosition
            };
            
            _playerMovementService.MoveTo(inputData);
        }
    }
}