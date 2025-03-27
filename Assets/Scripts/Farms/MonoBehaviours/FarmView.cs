using System;
using Jelewow.DNK.Farms.ScriptableObjects;
using UnityEngine;

namespace Jelewow.DNK.Farms.MonoBehaviours
{
    public class FarmView : MonoBehaviour
    {
        private int _size;
        
        [field: SerializeField]
        public FarmConfig FarmConfig { get; private set; }
        public float Scale => transform.localScale.x;

        public event Action<Resource, int> UpdateResource;
        
        private void Start()
        {
            UpdateResource?.Invoke(FarmConfig.Resource, _size);
        }

        public void ProduceResource(int amount)
        {
            _size += amount;
            UpdateResource?.Invoke(FarmConfig.Resource, _size);
        }

        public int CollectResource()
        {
            var size = _size;
            _size = 0;
            
            UpdateResource?.Invoke(FarmConfig.Resource, _size);
            return size;
        }
    }
}