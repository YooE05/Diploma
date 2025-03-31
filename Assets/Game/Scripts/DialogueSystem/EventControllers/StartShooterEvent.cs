using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DS.ScriptableObjects;
using YooE.DialogueSystem;
using YooE.Diploma.Interaction;
using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class StartShooterEvent : DialogueEventController
    {
        private readonly CubeHandler _cubeHandler;
        private readonly ScienceBaseGameController _scienceBaseGameController;

        public StartShooterEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceBaseGameController scienceBaseGameController) :
            base(dialogueState, dialogues)
        {
            _scienceBaseGameController = scienceBaseGameController;
        }

        protected override void FinishActions()
        {
            AsyncCountdown(1f, CancellationToken.None).Forget();
        }

        private async UniTask AsyncCountdown(float countdown, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(countdown), cancellationToken: token);

            _scienceBaseGameController.GoToShooterScene();
        }
    }

    public sealed class EnableFightDoorEvent : DialogueEventController
    {
        private readonly FightDoorInteractionComponent _fightDoor;

        public EnableFightDoorEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            FightDoorInteractionComponent fightDoor) :
            base(dialogueState, dialogues)
        {
            _fightDoor = fightDoor;
        }

        protected override void FinishActions()
        {
            _fightDoor.EnableInteractionAbility();
        }
    }

    public sealed class EnableMotionEvent : DialogueEventController
    {
        private readonly PlayerMotionController _playerMotionController;
        private readonly PlayerInteraction _playerInteraction;

        public EnableMotionEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            PlayerMotionController playerMotionController, PlayerInteraction playerInteraction) :
            base(dialogueState, dialogues)
        {
            _playerMotionController = playerMotionController;
            _playerInteraction = playerInteraction;
        }

        protected override void FinishActions()
        {
            _playerMotionController.EnableMotion();
            _playerInteraction.EnableInteraction();
        }
    }

    public sealed class GoNextMainDialogueEvent : DialogueEventController
    {
        private readonly CharactersDataHandler _charactersDataHandler;

        public GoNextMainDialogueEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            CharactersDataHandler charactersDataHandler) :
            base(dialogueState, dialogues)
        {
            _charactersDataHandler = charactersDataHandler;
        }

        protected override void FinishActions()
        {
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);
        }
    }

    public sealed class HideTaskPanelEvent : DialogueEventController
    {
        private readonly TaskPanel _taskPanel;

        public HideTaskPanelEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            TaskPanel taskPanel) :
            base(dialogueState, dialogues)
        {
            _taskPanel = taskPanel;
        }

        protected override void StartActions()
        {
            _taskPanel.Hide();
        }
    }

    public sealed class SaveGameEvent : DialogueEventController
    {
        private readonly SaveLoadManager _saveLoadManager;

        public SaveGameEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            SaveLoadManager saveLoadManager) :
            base(dialogueState, dialogues)
        {
            _saveLoadManager = saveLoadManager;
        }

        protected override void FinishActions()
        {
            _saveLoadManager.SaveGame();
        }
    }


    public sealed class EnableStage3InteractionsEvent : DialogueEventController
    {
        private readonly GardenViewController _gardenViewController;
        private readonly Stage3TaskTracker _taskTracker;

        public EnableStage3InteractionsEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            GardenViewController gardenViewController, Stage3TaskTracker taskTracker) :
            base(dialogueState, dialogues)
        {
            _gardenViewController = gardenViewController;
            _taskTracker = taskTracker;
        }

        protected override void FinishActions()
        {
            _gardenViewController.EnableStage3Interaction();
            _taskTracker.ShowTasksText();
        }
    }

    public sealed class DisableStage3InteractionsEvent : DialogueEventController
    {
        private readonly GardenViewController _gardenViewController;

        public DisableStage3InteractionsEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            GardenViewController gardenViewController) :
            base(dialogueState, dialogues)
        {
            _gardenViewController = gardenViewController;
        }

        protected override void FinishActions()
        {
            _gardenViewController.DisableStage3Interaction();
        }
    }

    public sealed class EnableStage4InteractionEvent : DialogueEventController
    {
        private readonly GardenViewController _gardenViewController;
        private readonly Stage4TaskTracker _taskTracker;

        public EnableStage4InteractionEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            GardenViewController gardenViewController, Stage4TaskTracker taskTracker) :
            base(dialogueState, dialogues)
        {
            _gardenViewController = gardenViewController;
            _taskTracker = taskTracker;
        }

        protected override void FinishActions()
        {
            _gardenViewController.EnableLeverAndGardenInteraction();
            _taskTracker.ShowTasksText();
        }
    }

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