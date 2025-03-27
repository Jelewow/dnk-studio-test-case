using UnityEngine;

namespace Jelewow.DNK.SaveLoadSystem.Services
{
    public class PlayerPrefsLoadService : ILoadSystemService
    {
        public SaveData Load()
        {
            var volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeKey, 1f);
            var saveData = new SaveData
            {
                Volume = volume
            };

            return saveData;
        }
    }
}