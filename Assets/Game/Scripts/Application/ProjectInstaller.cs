using UnityEngine;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DataSaveConfig _saveConfig;
        [SerializeField] private AudioManager _audioManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FileDataStreamer>().AsSingle().WithArguments(_saveConfig).NonLazy();
            Container.BindInterfacesTo<GameRepository>().AsSingle().NonLazy();

            Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
            
            Container.Bind<CharactersDataContainer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterDialoguesSaveLoader>().AsSingle().NonLazy();
            
            //Container.Bind<AudioManager>().FromInstance(_audioManager).AsSingle().NonLazy();
           // Container.Resolve<AudioManager>().CreateInstance();
            Container.BindInterfacesAndSelfTo<AudioSaveLoader>().AsSingle().NonLazy();
        }
    }
}