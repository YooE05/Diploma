using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class PlayerAttackInstaller : MonoInstaller
    {
        [SerializeField] private PlayerShooterView _playerView;

        [SerializeField] private TargetSensorConfig _targetSensorConfig;
        [SerializeField] private TargetsCollector _targetCollector;
        [SerializeField] private ShootingConfig _shootingConfig;

        [SerializeField] private GameObjectsPoolConfig _bulletsPoolConfig;
        [SerializeField] private Transform _bulletsInitParent;

        private Transform TargetsOverlapCenter => _playerView.Visual;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<BulletsSystem>().AsCached().WithArguments(_bulletsPoolConfig, _bulletsInitParent).NonLazy();
            Container.Bind<TargetsCollector>().FromInstance(_targetCollector).AsCached().NonLazy();
            Container.Bind<TargetPicker>().AsCached().WithArguments(_targetSensorConfig, TargetsOverlapCenter)
                .NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShooting>().AsCached()
                .WithArguments(_playerView.WeaponViews, _shootingConfig).NonLazy();
        }
    }
}