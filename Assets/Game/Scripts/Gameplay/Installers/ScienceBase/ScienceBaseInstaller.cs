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
        [SerializeField] private GardenViewController _gardenViewController;

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

            Container.Bind<PlayerInteraction>().FromInstance(_playerInteraction).AsCached()
                .NonLazy();
            Container.Bind<GardenViewController>().FromInstance(_gardenViewController).AsCached()
                .NonLazy();
        }
    }
}