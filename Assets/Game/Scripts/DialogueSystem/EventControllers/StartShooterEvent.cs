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

        /*protected override void FinishActions()
        {
            AsyncCountdown(1f, CancellationToken.None).Forget();
        }

        private async UniTask AsyncCountdown(float countdown, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(countdown), cancellationToken: token);

            _saveLoadManager.SaveGame();
        }*/
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
}