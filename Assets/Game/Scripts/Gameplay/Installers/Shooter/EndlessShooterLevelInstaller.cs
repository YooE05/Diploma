using UnityEngine;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class EndlessShooterLevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerShooterView _playerView;
        [SerializeField] private EndlessShooterGameLoop _shooterGameLoopController;
        [SerializeField] private bool IsSoundEnabled;

        [SerializeField] private ShooterGameplayScreenView _gameplayScreenView;
        [SerializeField] private ShooterPopupsView _popupsView;

        [SerializeField] private string _buttonsClickSoundName = "buttonClick";
        [SerializeField] private LoadingScreen _loadingScreen;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            //Container.BindInterfacesAndSelfTo<PlayerScoreSaveLoader2>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveLoadManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<LifecycleManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<EndlessShooterGameLoop>().FromInstance(_shooterGameLoopController)
                .AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<UpdateTimer>().AsCached().NonLazy();

            UI();
            Player();
        }

        private void UI()
        {
            Container.Bind<LoadingScreen>().FromInstance(_loadingScreen).AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<ShooterGameplayScreenPresenter>()
                .AsCached()
                .WithArguments(_gameplayScreenView)
                .NonLazy();
            Container.BindInterfacesAndSelfTo<SoundButtonPresenter>().AsCached()
                .WithArguments(_gameplayScreenView.SoundButtonView, _buttonsClickSoundName)
                .NonLazy();
            Container.BindInterfacesAndSelfTo<ShooterPopupsPresenter>().AsCached()
                .WithArguments(_popupsView, true)
                .NonLazy();
        }

        private void Player()
        {
            Container.BindInterfacesAndSelfTo<PlayerShooterInput>().AsSingle().NonLazy();
            Container.Bind<PlayerDeathObserver>().AsCached().WithArguments(true).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerShooterBrain>().AsCached().WithArguments(_playerView).NonLazy();
        }
    }
}