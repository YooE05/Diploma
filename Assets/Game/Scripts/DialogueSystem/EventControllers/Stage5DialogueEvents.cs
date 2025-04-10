using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;
using YooE.Diploma.Interaction;

namespace YooE.Diploma
{
    public sealed class EnableStage5InteractionsEvent : DialogueEventController
    {
        private readonly LockersViewController _lockersController;
        private readonly Stage5TaskTracker _taskTracker;


        public EnableStage5InteractionsEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            LockersViewController lockersController, Stage5TaskTracker taskTracker) :
            base(dialogueState, dialogues)
        {
            _lockersController = lockersController;
            _taskTracker = taskTracker;
        }

        protected override void FinishActions()
        {
            _lockersController.EnableLockersInteraction();
            _taskTracker.ShowTasksText();
        }
    }

    public sealed class Stage5GetNotepadEvent : DialogueEventController
    {
        private readonly Stage5TaskTracker _taskTracker;
        private readonly UIAnimationController _uiAnimationController;
        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerInteraction _playerInteraction;

        public Stage5GetNotepadEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            Stage5TaskTracker taskTracker, UIAnimationController uiAnimationController,
            PlayerMotionController playerMotionController, PlayerInteraction playerInteraction) :
            base(dialogueState, dialogues)
        {
            _uiAnimationController = uiAnimationController;
            _taskTracker = taskTracker;

            _playerMotionController = playerMotionController;
            _playerInteraction = playerInteraction;
        }

        protected override void FinishActions()
        {
            _uiAnimationController.OnItemPicked += NotepadPickedActions;
            _uiAnimationController.StartNotebookPickAnimation().Forget();
        }

        private void NotepadPickedActions()
        {
            _uiAnimationController.OnItemPicked -= NotepadPickedActions;
            _taskTracker.PickupNotepad();

            _playerMotionController.EnableMotion();
            _playerInteraction.EnableInteraction();
        }
    }

    public sealed class Stage5GetMeasureStickEvent : DialogueEventController
    {
        private readonly Stage5TaskTracker _taskTracker;
        private readonly UIAnimationController _uiAnimationController;
        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerInteraction _playerInteraction;

        public Stage5GetMeasureStickEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            Stage5TaskTracker taskTracker, UIAnimationController uiAnimationController,
            PlayerMotionController playerMotionController, PlayerInteraction playerInteraction) :
            base(dialogueState, dialogues)
        {
            _uiAnimationController = uiAnimationController;
            _taskTracker = taskTracker;

            _playerMotionController = playerMotionController;
            _playerInteraction = playerInteraction;
        }

        protected override void FinishActions()
        {
            _uiAnimationController.OnItemPicked += StickPickedActions;
            _uiAnimationController.StartMeasureStickPickAnimation().Forget();
        }

        private void StickPickedActions()
        {
            _uiAnimationController.OnItemPicked -= StickPickedActions;
            _taskTracker.PickupMeasureStick();

            _playerMotionController.EnableMotion();
            _playerInteraction.EnableInteraction();
        }
    }

    public sealed class EnableGardenStage5InteractionsEvent : DialogueEventController
    {
        private readonly GardenViewController _gardenViewController;
        private readonly Stage5TaskTracker _taskTracker;

        public EnableGardenStage5InteractionsEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            GardenViewController gardenViewController, Stage5TaskTracker taskTracker) :
            base(dialogueState, dialogues)
        {
            _gardenViewController = gardenViewController;
            _taskTracker = taskTracker;
        }

        protected override void FinishActions()
        {
            _gardenViewController.EnableStage5Interaction();
            _taskTracker.ShowMeasurePlantsTaskText();
        }
    }

    public sealed class Stage5ShowWaitAnimationEvent : DialogueEventController
    {
        private readonly UIAnimationController _uiAnimations;
        private readonly CharactersDataHandler _charactersDataHandler;

        private readonly CharacterDialogueComponent _mainNPC;

        public Stage5ShowWaitAnimationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            UIAnimationController uiAnimations, CharactersDataHandler charactersDataHandler,
            CharacterDialogueComponent mainNPC) :
            base(dialogueState, dialogues)
        {
            _uiAnimations = uiAnimations;
            _charactersDataHandler = charactersDataHandler;
            _mainNPC = mainNPC;
        }

        protected override void FinishActions()
        {
            _uiAnimations.ShowWaitingFade();
            _uiAnimations.OnWaitingEnd += AfterWaitingActions;
        }

        private void AfterWaitingActions()
        {
            _uiAnimations.OnWaitingEnd -= AfterWaitingActions;
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);

            _mainNPC.StartCurrentDialogueGroup().Forget();
        }
    }
}