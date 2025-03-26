using UnityEngine;
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
        }
    }
}