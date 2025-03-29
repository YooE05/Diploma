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
        [SerializeField] private VariablesPopup _variablesPopup;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DialogueView>().FromInstance(_dialogueView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DialogueState>().AsSingle().WithArguments(_dialogueContainer).NonLazy();

            Container.Resolve<CharactersDataHandler>().AddCharacters(_characters);

            Container.Bind<ScienceMethodPopup>().FromInstance(_scienceMethodPopup).NonLazy();
            Container.Bind<VariablesPopup>().FromInstance(_variablesPopup).NonLazy();

            TaskTrackers();
        }

        private void TaskTrackers()
        {
            Container.BindInterfacesAndSelfTo<Stage3TaskTracker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Stage4TaskTracker>().AsSingle().NonLazy();
        }
    }
}