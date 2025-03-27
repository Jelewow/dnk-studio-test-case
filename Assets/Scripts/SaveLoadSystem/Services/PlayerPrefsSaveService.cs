using UnityEngine;

namespace Jelewow.DNK.SaveLoadSystem.Services
{
    public class PlayerPrefsSaveService : ISaveSystemService
    {
        public void Save(SaveData data)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.VolumeKey, data.Volume);
            PlayerPrefs.Save();
        }
    }
}