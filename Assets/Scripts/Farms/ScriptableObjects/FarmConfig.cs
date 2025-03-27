using UnityEngine;

namespace Jelewow.DNK.Farms.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Farm", menuName = "Game/Farm/Farm")]
    public class FarmConfig : ScriptableObject
    {
        [field: SerializeField]
        public Resource Resource { get; private set; }
        
        [field: SerializeField]
        public float TickRate { get; private set; }
        
        [field: SerializeField]
        public int ProducePerTick { get; private set; }
    }
}