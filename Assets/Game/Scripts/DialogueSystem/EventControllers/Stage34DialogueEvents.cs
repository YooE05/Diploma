using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class EnableStage3InteractionsEvent : DialogueEvent
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

    public sealed class DisableStage3InteractionsEvent : DialogueEvent
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

    public sealed class EnableStage4InteractionEvent : DialogueEvent
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
}