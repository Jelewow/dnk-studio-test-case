using UnityEngine;

namespace Jelewow.DNK.Player.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Config", menuName = "Game/Player/Settings")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}