using Jelewow.DNK.Farms.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Jelewow.DNK.Farms.MonoBehaviours
{
    public class FarmUIInfo : MonoBehaviour
    {
        [SerializeField] private FarmView _farmView;
        [SerializeField] private TMP_Text _info;

        private void OnEnable()
        {
            _farmView.UpdateResource += UpdateUI;
        }

        private void OnDisable()
        {
            _farmView.UpdateResource -= UpdateUI;
        }

        private void UpdateUI(Resource resource, int size)
        {
            _info.text = $"{resource.Name} {size}";
        }
    }
}