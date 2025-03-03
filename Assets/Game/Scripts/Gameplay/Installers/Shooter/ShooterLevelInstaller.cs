using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterLevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerShooterView _playerView;
        [SerializeField] private ShooterGameLoopController _shooterGameLoopController;
        [SerializeField] private SceneAudioSystem _sceneAudioSystem;
        [SerializeField] private bool IsSoundEnabled;

        [SerializeField] private ShooterGameplayScreenView _gameplayScreenView;
        [SerializeField] private ShooterPopupsView _popupsView;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LifecycleManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<ShooterGameLoopController>().FromInstance(_shooterGameLoopController)
                .AsCached()
                .NonLazy();

            Container.Bind<SceneAudioSystem>().FromInstance(_sceneAudioSystem).AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<UpdateTimer>().AsCached().NonLazy();

            UI();
            Player();
        }

        private void UI()
        {
            Container.BindInterfacesAndSelfTo<ShooterGameplayScreenPresenter>().AsCached()
                .WithArguments(_gameplayScreenView)
                .NonLazy();
            Container.BindInterfacesAndSelfTo<ShooterPopupsPresenter>().AsCached()
                .WithArguments(_popupsView)
                .NonLazy();
        }

        private void Player()
        {
            Container.Bind<PlayerDeathObserver>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShooterBrain>().AsCached().WithArguments(_playerView).NonLazy();
        }
    }
}