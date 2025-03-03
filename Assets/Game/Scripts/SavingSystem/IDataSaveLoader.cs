using Zenject;

namespace YooE.SaveLoad
{
    public interface IDataSaveLoader
    {
        public void LoadData(IGameRepository gameRepository, DiContainer container);

        public void SaveData(IGameRepository gameRepository, DiContainer container);
    }
}