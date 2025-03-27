using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class DialogueEventInstaller : MonoInstaller
    {
        [SerializeField] private List<DSDialogueSO> _saveGameDialogues;

        [SerializeField] private List<DSDialogueSO> _showCubeDialogues;
        [SerializeField] private List<DSDialogueSO> _startShooterDialogues;
        [SerializeField] private List<DSDialogueSO> _enableMotionDialogues;
        [SerializeField] private List<DSDialogueSO> _goNextMainQuestDialogues;

        [SerializeField] private List<DSDialogueSO> _stage3CheckGardenDialogues;
        [SerializeField] private List<DSDialogueSO> _stage3CheckPlantDialogues;

        [Header("SсienceMethod")]
        [SerializeField]
        private List<DSDialogueSO> _showSMPopupDialogues;
        [SerializeField] private List<DSDialogueSO> _hideSMPopupDialogues;
        [SerializeField] private List<DSDialogueSO> _enableObservationDialogues;
        [SerializeField] private List<DSDialogueSO> _enableHypothesisDialogues;
        [SerializeField] private List<DSDialogueSO> _enableExperimentDialogues;
        [SerializeField] private List<DSDialogueSO> _enableAnalysisDialogues;
        [SerializeField] private List<DSDialogueSO> _enableConclusionDialogues;

        public override void InstallBindings()
        {
            Container.Bind<SaveGameEvent>().AsCached().WithArguments(_saveGameDialogues).NonLazy();

            Container.Bind<ShowHideCubeEvent>().AsCached().WithArguments(_showCubeDialogues).NonLazy();
            Container.Bind<StartShooterEvent>().AsCached().WithArguments(_startShooterDialogues).NonLazy();
            Container.Bind<EnableMotionEvent>().AsCached().WithArguments(_enableMotionDialogues).NonLazy();
            Container.Bind<GoNextMainDialogueEvent>().AsCached().WithArguments(_goNextMainQuestDialogues).NonLazy();

            Container.Bind<Stage3TaskCheckGardenEvent>().AsCached().WithArguments(_stage3CheckGardenDialogues)
                .NonLazy();
            Container.Bind<Stage3TaskCheckSeparatePlantEvent>().AsCached().WithArguments(_stage3CheckPlantDialogues)
                .NonLazy();

            ScienceMethod();
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
    }
}