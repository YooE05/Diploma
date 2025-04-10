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
        [SerializeField] private DataPopup _dataPopup;
        [SerializeField] private TaskPanel _taskPanel;
        [SerializeField] private GraphPanelInteraction _graphPanel;
        [SerializeField] private AverageNotesAndConclusionUI _averageNotesPanel;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DialogueView>().FromInstance(_dialogueView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DialogueState>().AsSingle().WithArguments(_dialogueContainer).NonLazy();

            Container.Resolve<CharactersDataHandler>().AddCharacters(_characters);

            Container.BindInterfacesAndSelfTo<ScienceMethodPopup>().FromInstance(_scienceMethodPopup).NonLazy();
            Container.BindInterfacesAndSelfTo<VariablesPopup>().FromInstance(_variablesPopup).NonLazy();
            Container.BindInterfacesAndSelfTo<DataPopup>().FromInstance(_dataPopup).NonLazy();

            Container.BindInterfacesAndSelfTo<GraphPanelInteraction>().FromInstance(_graphPanel).NonLazy();
            Container.BindInterfacesAndSelfTo<AverageNotesAndConclusionUI>().FromInstance(_averageNotesPanel).NonLazy();

            TaskTrackers();
        }

        private void TaskTrackers()
        {
            Container.BindInterfacesAndSelfTo<TaskPanel>().FromInstance(_taskPanel).NonLazy();

            Container.BindInterfacesAndSelfTo<Stage3TaskTracker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Stage4TaskTracker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Stage5TaskTracker>().AsSingle().NonLazy();
        }
    }
}