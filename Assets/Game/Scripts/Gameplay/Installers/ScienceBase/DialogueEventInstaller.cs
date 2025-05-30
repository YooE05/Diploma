using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma
{
    public class DialogueEventInstaller : MonoInstaller
    {
        [SerializeField] private StoreManager _storeManager;
        [SerializeField] private CharacterDialogueComponent _mainNPC;

        [SerializeField] private List<DSDialogueSO> _saveGameDialogues;

        [SerializeField] private List<DSDialogueSO> _startShooterDialogues;
        [SerializeField] private List<DSDialogueSO> _enableMotionDialogues;
        [SerializeField] private List<DSDialogueSO> _goNextMainQuestDialogues;

        [SerializeField] private List<DSDialogueSO> _enableFightDoorDialogues;
        [SerializeField] private List<DSDialogueSO> _hideTaskPanelDialogues;

        [SerializeField] private List<DSDialogueSO> _stage3CheckGardenDialogues;
        [SerializeField] private List<DSDialogueSO> _stage3CheckPlantDialogues;

        [SerializeField] private List<DSDialogueSO> _stage3EnableInteractionDialogues;
        [SerializeField] private List<DSDialogueSO> _stage3DisableInteractionDialogues;

        [SerializeField] private List<DSDialogueSO> _stage4EnableInteractionDialogues;

        [SerializeField] private List<DSDialogueSO> _stage5EnableItemsDialogues;
        [SerializeField] private List<DSDialogueSO> _stage5EnableGardenInteractionsDialogues;
        [SerializeField] private List<DSDialogueSO> _stage5ShowWaitAnimationDialogues;
        [SerializeField] private List<DSDialogueSO> _stage5GetMeasureStickDialogues;
        [SerializeField] private List<DSDialogueSO> _stage5GetNotepadDialogues;

        [SerializeField] private List<DSDialogueSO> _completeGameDialogues;

        //Popups
        [Header("Popups")] [SerializeField] private List<DSDialogueSO> _showIndependentVarDialogues;
        [SerializeField] private List<DSDialogueSO> _showDependentVarDialogues;
        [SerializeField] private List<DSDialogueSO> _hideVarsDialogues;

        [SerializeField] private List<DSDialogueSO> _showQualityDataDialogues;
        [SerializeField] private List<DSDialogueSO> _showQuantitativeDataDialogues;
        [SerializeField] private List<DSDialogueSO> _hideDataDialogues;

        [SerializeField] private List<DSDialogueSO> _showAverageNotesDialogues;
        [SerializeField] private List<DSDialogueSO> _hideAverageNotesDialogues;
        [SerializeField] private List<DSDialogueSO> _showGraphDialogues;
        [SerializeField] private List<DSDialogueSO> _hideLittleGraphDialogues;
        [SerializeField] private List<DSDialogueSO> _showConclusionPopupDialogues;
        [SerializeField] private List<DSDialogueSO> _hideConclusionPopupDialogues;

        [Header("ScienceMethod")]
        //ScienceMethod
        [SerializeField]
        private List<DSDialogueSO> _showSMPopupDialogues;

        [SerializeField] private List<DSDialogueSO> _hideSMPopupDialogues;
        [SerializeField] private List<DSDialogueSO> _enableObservationDialogues;
        [SerializeField] private List<DSDialogueSO> _enableHypothesisDialogues;
        [SerializeField] private List<DSDialogueSO> _enableExperimentDialogues;
        [SerializeField] private List<DSDialogueSO> _enableAnalysisDialogues;
        [SerializeField] private List<DSDialogueSO> _enableConclusionDialogues;

        [Header("Navigation")] [SerializeField] private List<DSDialogueSO> _hideNavigation;
        [SerializeField] private List<DSDialogueSO> _showNPCNavigation;
        [SerializeField] private List<DSDialogueSO> _showDoorNavigation;
        [SerializeField] private List<DSDialogueSO> _showSeedNavigation;
        [SerializeField] private List<DSDialogueSO> _showLeverNavigation;

        public override void InstallBindings()
        {
            Container.Bind<SaveGameEvent>().AsCached().WithArguments(_saveGameDialogues).NonLazy();
            Container.Bind<CompleteGameEvent>().AsCached().WithArguments(_completeGameDialogues, _storeManager)
                .NonLazy();

            Container.Bind<EnableFightDoorEvent>().AsCached().WithArguments(_enableFightDoorDialogues).NonLazy();
            Container.Bind<StartShooterEvent>().AsCached().WithArguments(_startShooterDialogues).NonLazy();
            Container.Bind<EnableMotionEvent>().AsCached().WithArguments(_enableMotionDialogues).NonLazy();
            Container.Bind<GoNextMainDialogueEvent>().AsCached().WithArguments(_goNextMainQuestDialogues).NonLazy();
            Container.Bind<HideTaskPanelEvent>().AsCached().WithArguments(_hideTaskPanelDialogues).NonLazy();

            Stage34();
            Stage5();
            Stage6();

            DialoguePopups();
            Navigation();
        }

        private void Stage34()
        {
            Container.Bind<Stage3TaskCheckGardenEvent>().AsCached().WithArguments(_stage3CheckGardenDialogues)
                .NonLazy();
            Container.Bind<Stage3TaskCheckSeparatePlantEvent>().AsCached().WithArguments(_stage3CheckPlantDialogues)
                .NonLazy();

            Container.Bind<EnableStage3InteractionsEvent>().AsCached()
                .WithArguments(_stage3EnableInteractionDialogues)
                .NonLazy();
            Container.Bind<DisableStage3InteractionsEvent>().AsCached()
                .WithArguments(_stage3DisableInteractionDialogues)
                .NonLazy();

            Container.Bind<EnableStage4InteractionEvent>().AsCached()
                .WithArguments(_stage4EnableInteractionDialogues)
                .NonLazy();
        }

        private void Stage5()
        {
            Container.Bind<EnableStage5InteractionsEvent>().AsCached()
                .WithArguments(_stage5EnableItemsDialogues)
                .NonLazy();
            Container.Bind<EnableGardenStage5InteractionsEvent>().AsCached()
                .WithArguments(_stage5EnableGardenInteractionsDialogues)
                .NonLazy();
            Container.Bind<Stage5ShowWaitAnimationEvent>().AsCached()
                .WithArguments(_stage5ShowWaitAnimationDialogues, _mainNPC)
                .NonLazy();
            Container.Bind<Stage5GetMeasureStickEvent>().AsCached()
                .WithArguments(_stage5GetMeasureStickDialogues)
                .NonLazy();
            Container.Bind<Stage5GetNotepadEvent>().AsCached()
                .WithArguments(_stage5GetNotepadDialogues)
                .NonLazy();
        }

        private void Stage6()
        {
            Container.Bind<Stage6ShowAverageNotesEvent>().AsCached()
                .WithArguments(_showAverageNotesDialogues)
                .NonLazy();
            Container.Bind<Stage6HideAverageNotesEvent>().AsCached()
                .WithArguments(_hideAverageNotesDialogues)
                .NonLazy();
            Container.Bind<Stage6ShowGraphEvent>().AsCached()
                .WithArguments(_showGraphDialogues)
                .NonLazy();        
            Container.Bind<Stage6HideLittleGraphEvent>().AsCached()
                .WithArguments(_hideLittleGraphDialogues)
                .NonLazy();
            Container.Bind<Stage6ShowConclusionPopupEvent>().AsCached()
                .WithArguments(_showConclusionPopupDialogues)
                .NonLazy();
            Container.Bind<Stage6HideConclusionPopupEvent>().AsCached()
                .WithArguments(_hideConclusionPopupDialogues)
                .NonLazy();
        }

        private void DialoguePopups()
        {
            ScienceMethod();

            Container.Bind<ShowIndependentVarEvent>().AsCached().WithArguments(_showIndependentVarDialogues).NonLazy();
            Container.Bind<ShowDependentVarEvent>().AsCached().WithArguments(_showDependentVarDialogues).NonLazy();
            Container.Bind<HideVariablesEvent>().AsCached().WithArguments(_hideVarsDialogues).NonLazy();

            Container.Bind<ShowQualityDataEvent>().AsCached().WithArguments(_showQualityDataDialogues).NonLazy();
            Container.Bind<ShowQuantitativeDataEvent>().AsCached().WithArguments(_showQuantitativeDataDialogues)
                .NonLazy();
            Container.Bind<HideDataEvent>().AsCached().WithArguments(_hideDataDialogues).NonLazy();
        }

        private void ScienceMethod()
        {
            Container.Bind<ShowScienceMethodPopupEvent>().AsCached().WithArguments(_showSMPopupDialogues).NonLazy();
            Container.Bind<HideScienceMethodPopupEvent>().AsCached().WithArguments(_hideSMPopupDialogues).NonLazy();

            Container.Bind<EnableObservationEvent>().AsCached().WithArguments(_enableObservationDialogues).NonLazy();
            Container.Bind<EnableHypothesisEvent>().AsCached().WithArguments(_enableHypothesisDialogues).NonLazy();
            Container.Bind<EnableExperimentEvent>().AsCached().WithArguments(_enableExperimentDialogues).NonLazy();
            Container.Bind<EnableAnalysisEvent>().AsCached().WithArguments(_enableAnalysisDialogues).NonLazy();
            Container.Bind<EnableConclusionEvent>().AsCached().WithArguments(_enableConclusionDialogues).NonLazy();
        }

        private void Navigation()
        {
            Container.Bind<HideNavigationEvent>().AsCached().WithArguments(_hideNavigation).NonLazy();

            Container.Bind<SetDoorNavigationEvent>().AsCached().WithArguments(_showDoorNavigation).NonLazy();
            Container.Bind<SetNPCNavigationEvent>().AsCached().WithArguments(_showNPCNavigation).NonLazy();
            Container.Bind<SetSeedNavigationEvent>().AsCached().WithArguments(_showSeedNavigation).NonLazy();
            Container.Bind<SetLeverNavigationEvent>().AsCached().WithArguments(_showLeverNavigation).NonLazy();
        }
    }
}