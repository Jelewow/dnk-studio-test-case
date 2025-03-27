using UnityEngine;

namespace Jelewow.DNK.Farms.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Game/Farm/Resource")]
    public class Resource : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
    }
}