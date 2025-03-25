using UnityEngine;

namespace Jelewow.DNK.GameCamera.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Camera Config", menuName = "Game/Player/Camera")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [field: SerializeField]
        public float Speed { get; private set; }
        
        [field: SerializeField]
        public float BorderThickness { get; private set; }
        
        [field: SerializeField]
        public Vector2 Border { get; private set; }
    }
}