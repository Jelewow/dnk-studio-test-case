using UnityEngine;

namespace Jelewow.DNK.Player.Types
{
    public struct PlayerInputData
    {
        public Vector3 MousePosition;
        public float Vertical;
        public float Horizontal;
        
        public Vector2 TapPosition;
        public Vector2 TapStartPosition;
        public Vector2 RawTapMovingPosition;
        
        public Touch Touch;
        public int TouchCount;

    }
}