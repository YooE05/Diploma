using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterLevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerShooterView _playerView;
        [SerializeField] private GameLoopController _gameLoopController;
        [SerializeField] private AudioSystem _audioSystem;
        [SerializeField] private bool IsSoundEnabled;

        [SerializeField] private ShooterGameplayScreenView _gameplayScreenView;
        [SerializeField] private ShooterPopupsView _popupsView;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LifecycleManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<GameLoopController>().FromInstance(_gameLoopController).AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<UpdateTimer>().AsCached().NonLazy();

            UI();
            Player();
        }

        private void UI()
        {
            Container.Bind<ShooterGameplayScreenPresenter>().AsCached()
                .WithArguments(_audioSystem, _gameplayScreenView, IsSoundEnabled)
                .NonLazy();
            Container.Bind<ShooterPopupsPresenter>().AsCached()
                .WithArguments(_popupsView)
                .NonLazy();
        }

        private void Player()
        {
            Container.Bind<PlayerDeathObserver>().AsCached().NonLazy();
            Container.Bind<PlayerShooterBrain>().AsCached().WithArguments(_playerView).NonLazy();
        }
    }
}