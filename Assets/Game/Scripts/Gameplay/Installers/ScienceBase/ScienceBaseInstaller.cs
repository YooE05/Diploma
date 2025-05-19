using Game.Tutorial.Gameplay;
using UnityEngine;
using YooE.Diploma.Interaction;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public class ScienceBaseInstaller : MonoInstaller
    {
        [SerializeField] private CubeHandler _cubeHandler;
        [SerializeField] private ScienceBaseGameController _gameController;

        [SerializeField] private SoundButtonView _soundButtonView;
        [SerializeField] private string _buttonsClickSoundName = "buttonClick";

        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private UIAnimationController _uiAnimationController;
        [SerializeField] private GardenViewController _gardenViewController;
        [SerializeField] private LockersViewController _lockersViewController;

        [SerializeField] private FightDoorInteractionComponent _fightDoorInteraction;

        [SerializeField] private NavigationManager _navigationManager;
        [SerializeField] private StoreManager _storeManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LifecycleManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveLoadManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<ScienceBaseGameController>().FromInstance(_gameController).AsCached()
                .NonLazy();
            Container.Bind<CubeHandler>().FromInstance(_cubeHandler).AsCached().NonLazy();

            Container.BindInterfacesAndSelfTo<SoundButtonPresenter>().AsCached()
                .WithArguments(_soundButtonView, _buttonsClickSoundName)
                .NonLazy();
            Container.BindInterfacesAndSelfTo<UIAnimationController>().FromInstance(_uiAnimationController).AsCached()
                .NonLazy();


            Container.Bind<PlayerInteraction>().FromInstance(_playerInteraction).AsCached()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<GardenViewController>().FromInstance(_gardenViewController).AsCached()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<LockersViewController>().FromInstance(_lockersViewController).AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<FightDoorInteractionComponent>().FromInstance(_fightDoorInteraction)
                .AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<NavigationManager>().FromInstance(_navigationManager).AsCached()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<StoreManager>().FromInstance(_storeManager).AsCached()
                .NonLazy();
        }
    }
}