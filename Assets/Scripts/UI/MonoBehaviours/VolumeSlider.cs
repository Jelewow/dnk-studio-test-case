using Jelewow.DNK.SaveLoadSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Jelewow.DNK.UI.MonoBehaviours
{
    public class VolumeSlider : MonoBehaviour
    {
        [Inject] private readonly ISaveSystemService _saveSystem;
        
        [SerializeField] private Slider _slider;
        
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(delegate { OnVolumeChanged(); });
        }

        public void LoadVolume(SaveData data)
        {
            _slider.value = data.Volume;
            ApplyVolume(data.Volume);
        }
        
        private void OnVolumeChanged()
        {
            var volume = _slider.value;
            ApplyVolume(volume);
            _saveSystem.Save(new SaveData{Volume = volume});
        }

        private void ApplyVolume(float volume)
        {
            AudioListener.volume = volume;
        }
    }
}