using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _commonEnemyConfig;
        [SerializeField] private EnemyPoolsSettings _enemyPoolsSettings;
        [SerializeField] private Transform _enemiesParent;

        private List<EnemySpawnPoint> _enemySpawnPoints = new();

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            _enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>().ToList();

            Container.Bind<EnemyConfig>().FromInstance(_commonEnemyConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsCached()
                .WithArguments(_enemySpawnPoints, _enemyPoolsSettings, _enemiesParent).NonLazy();
            Container.BindInterfacesAndSelfTo<EnemiesInitializer>().AsCached().NonLazy();
            Container.Bind<EnemyWaveObserver>().AsCached().NonLazy();
        }
    }
}