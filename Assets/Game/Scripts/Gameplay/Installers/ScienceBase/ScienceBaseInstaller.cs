using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class ScienceBaseInstaller : MonoInstaller
    {
        [SerializeField] private CubeHandler _cubeHandler;
        [SerializeField] private ScienceBaseGameController _gameController;

        public override void InstallBindings()
        {
            Container.Bind<CubeHandler>().FromInstance(_cubeHandler).AsCached().NonLazy();
            Container.Bind<ScienceBaseGameController>().FromInstance(_gameController).AsCached().NonLazy();
        }
    }
}