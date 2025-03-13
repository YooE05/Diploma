using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.SaveLoad;
using Zenject;

namespace YooE.DialogueSystem
{
    public class DialogueSystemInstaller : MonoInstaller
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private DSDialogueContainerSO _dialogueContainer;
        [SerializeField] private List<CharacterDialogueComponent> _characters;

        public override void InstallBindings()
        {
            Container.Bind<CharactersDataContainer>().AsSingle().WithArguments(_characters).NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterDialoguesSaveLoader>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<DialogueView>().FromInstance(_dialogueView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DialogueState>().AsSingle().WithArguments(_dialogueContainer).NonLazy();
        }
    }
}