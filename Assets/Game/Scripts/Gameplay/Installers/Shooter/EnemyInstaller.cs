using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _commonEnemyConfig;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromInstance(_commonEnemyConfig).AsSingle().NonLazy();
        }
    }
}