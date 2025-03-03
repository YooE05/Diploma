using UnityEngine;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public class ScienceBaseInstaller : MonoInstaller
    {
        [SerializeField] private CubeHandler _cubeHandler;
        [SerializeField] private ScienceBaseGameController _gameController;
        [SerializeField] private SceneAudioSystem _sceneAudioSystem;
        [SerializeField] private SoundButtonView _soundButtonView;

        public override void InstallBindings()
        {
            Container.Bind<ScienceBaseGameController>().FromInstance(_gameController).AsCached().NonLazy();
            Container.Bind<CubeHandler>().FromInstance(_cubeHandler).AsCached().NonLazy();
            Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
            // Container.BindInterfacesTo<CharacterDialoguesSaveLoader>().AsCached().NonLazy();

            Container.Bind<SceneAudioSystem>().FromInstance(_sceneAudioSystem).AsCached().NonLazy();
            Container.Bind<SoundButtonPresenter>().AsCached().WithArguments(_soundButtonView).NonLazy();
        }
    }
}