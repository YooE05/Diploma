using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.Diploma;
using Zenject;

namespace YooE.DialogueSystem
{
    public class DialogueSystemInstaller : MonoInstaller
    {
        [SerializeField] private DialogueView _dialogueView;
        [SerializeField] private DSDialogueContainerSO _dialogueContainer;
        [SerializeField] private List<CharacterDialogueComponent> _characters;
        [SerializeField] private ScienceMethodPopup _scienceMethodPopup;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DialogueView>().FromInstance(_dialogueView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DialogueState>().AsSingle().WithArguments(_dialogueContainer).NonLazy();

            Container.Resolve<CharactersDataHandler>().AddCharacters(_characters);

            Container.Bind<ScienceMethodPopup>().FromInstance(_scienceMethodPopup).NonLazy();

            Stage3();
        }

        private void Stage3()
        {
            Container.BindInterfacesAndSelfTo<Stage3TaskTracker>().AsSingle().NonLazy();
        }
    }
}