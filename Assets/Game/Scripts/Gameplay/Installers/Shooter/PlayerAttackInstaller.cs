using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class PlayerAttackInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;

        [SerializeField] private TargetSensorConfig _targetSensorConfig;
        [SerializeField] private ShootingConfig _shootingConfig;

        [SerializeField] private GameObjectsPoolConfig _bulletsPoolConfig;
        [SerializeField] private Transform _bulletsInitParent;

        private Transform TargetsOverlapCenter => _playerView.Visual;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<BulletsSystem>().AsSingle().WithArguments(_bulletsPoolConfig, _bulletsInitParent).NonLazy();
            Container.Bind<TargetSensor>().AsSingle().WithArguments(_targetSensorConfig)
                .NonLazy();
            Container.Bind<TargetPicker>().AsSingle().WithArguments(TargetsOverlapCenter).NonLazy();
            Container.BindInterfacesTo<PlayerShooting>().AsSingle()
                .WithArguments(_playerView.WeaponViews, _shootingConfig).NonLazy();
        }
    }
}