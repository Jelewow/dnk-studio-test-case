using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jelewow.DNK.Farms.MonoBehaviours
{
    public class FarmContainer : MonoBehaviour
    {
        [SerializeField] private List<FarmView> _farms;

        public List<FarmView> Farms => _farms;
        
        [ContextMenu("Fill Farms")]
        private void FillSpawnPoints()
        {
            _farms = GetComponentsInChildren<FarmView>().ToList();
        }
    }
}