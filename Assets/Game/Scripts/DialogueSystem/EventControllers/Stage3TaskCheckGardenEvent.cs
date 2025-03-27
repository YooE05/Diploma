using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class Stage3TaskCheckGardenEvent : DialogueEventController
    {
        private readonly Stage3TaskTracker _taskTracker;

        public Stage3TaskCheckGardenEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            Stage3TaskTracker taskTracker) :
            base(dialogueState, dialogues)
        {
            _taskTracker = taskTracker;
        }

        protected override void FinishActions()
        {
            _taskTracker.CheckGarden();
        }
    }

    public sealed class Stage3TaskCheckSeparatePlantEvent : DialogueEventController
    {
        private readonly Stage3TaskTracker _taskTracker;

        public Stage3TaskCheckSeparatePlantEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            Stage3TaskTracker taskTracker) :
            base(dialogueState, dialogues)
        {
            _taskTracker = taskTracker;
        }

        protected override void FinishActions()
        {
            _taskTracker.CheckSeparatePlant();
        }
    }
}