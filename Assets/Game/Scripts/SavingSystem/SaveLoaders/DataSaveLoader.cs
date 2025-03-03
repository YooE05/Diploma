using Zenject;

namespace YooE.SaveLoad
{
    public abstract class DataSaveLoader<TData, TService> : IDataSaveLoader
    {
        void IDataSaveLoader.SaveData(IGameRepository gameRepository, DiContainer container)
        {
            var service = container.Resolve<TService>();
            TData data = GetData(service);
            gameRepository.SetData(data);
        }

        void IDataSaveLoader.LoadData(IGameRepository gameRepository, DiContainer container)
        {
            var service = container.Resolve<TService>();
            
            if (!gameRepository.TryGetData(out TData data))
            {
                SetDefaultData(service);
                return;
            }

            SetData(service, data);
        }

        protected virtual void SetDefaultData(TService service) { }
        protected abstract TData GetData(TService service);
        protected abstract void SetData(TService service, TData data);
    }
}