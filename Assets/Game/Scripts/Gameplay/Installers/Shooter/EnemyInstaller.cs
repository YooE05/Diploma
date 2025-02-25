using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _commonEnemyConfig;
        private List<EnemyView> _enemyViews = new();

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            _enemyViews = FindObjectsOfType<EnemyView>().ToList();

            Container.Bind<EnemyConfig>().FromInstance(_commonEnemyConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyContainer>().AsCached()
                .WithArguments(_enemyViews, _commonEnemyConfig).NonLazy();
            Container.Bind<EnemyWaveObserver>().AsCached().NonLazy();
        }
    }
}