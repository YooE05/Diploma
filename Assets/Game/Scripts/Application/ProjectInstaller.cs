using Audio;
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

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FileDataStreamer>().AsSingle().WithArguments(_saveConfig).NonLazy();
            Container.BindInterfacesTo<GameRepository>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AudioManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AudioSaveLoader>().AsSingle().NonLazy();

            Container.Bind<CharactersDataHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterDialoguesSaveLoader>().AsSingle().NonLazy();

            Container.Bind<PlayerDataContainer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerDataSaveLoader>().AsSingle().NonLazy();
            
            Container.Bind<ShooterLoader>().AsSingle().NonLazy();
        }
    }
}