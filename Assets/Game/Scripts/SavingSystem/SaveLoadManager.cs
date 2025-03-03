using System.Collections.Generic;
using Zenject;

namespace YooE.SaveLoad
{
    public sealed class SaveLoadManager
    {
        private readonly IGameRepository _gameRepository;
        private readonly DiContainer _container;
        private readonly List<IDataSaveLoader> _dataSaveLoaders;

        public SaveLoadManager(IGameRepository gameRepository, DiContainer container,
            List<IDataSaveLoader> dataSaveLoaders)
        {
            _gameRepository = gameRepository;
            _container = container;
            _dataSaveLoaders = dataSaveLoaders;
        }

        public void AddDataSaveLoader(IDataSaveLoader saveLoader)
        {
            if (!_dataSaveLoaders.Contains(saveLoader))
            {
                _dataSaveLoaders.Add(saveLoader);
            }
        }

        public void RemoveDataSaveLoader(IDataSaveLoader saveLoader)
        {
            if (_dataSaveLoaders.Contains(saveLoader))
            {
                _dataSaveLoaders.Remove(saveLoader);
            }
        }

        public void SaveGame()
        {
            for (var i = 0; i < _dataSaveLoaders.Count; i++)
            {
                _dataSaveLoaders[i].SaveData(_gameRepository, _container);
            }

            _gameRepository.SaveState();
        }

        public void LoadGame()
        {
            _gameRepository.LoadState();

            for (var i = 0; i < _dataSaveLoaders.Count; i++)
            {
                _dataSaveLoaders[i].LoadData(_gameRepository, _container);
            }
        }

        public void ResetGame()
        {
            _gameRepository.ResetState();
            LoadGame();
        }
    }
}