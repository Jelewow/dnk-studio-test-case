namespace Jelewow.DNK.SaveLoadSystem
{
    public interface ISaveSystemService
    {
        void Save(SaveData data);
    }

    public interface ILoadSystemService
    {
        SaveData Load();
    }
}