using System.Collections.Generic;
using DS.ScriptableObjects;

namespace YooE.DialogueSystem
{
    public abstract class DialogueEvent
    {
        protected readonly DialogueState _dialogueState;
        protected readonly List<DSDialogueSO> _dialogues = new();

        protected DialogueEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues)
        {
            _dialogueState = dialogueState;
            _dialogues.AddRange(dialogues);

            _dialogueState.OnDialogueStart += OnDialogueStart;
            _dialogueState.OnDialogueFinished += OnDialogueFinish;
        }

        private void OnDialogueStart(DSDialogueSO dialogue)
        {
            if (_dialogues.Contains(dialogue))
                StartActions();
        }

        private void OnDialogueFinish(DSDialogueSO dialogue)
        {
            if (_dialogues.Contains(dialogue))
                FinishActions();
        }

        protected virtual void StartActions()
        {
        }

        protected virtual void FinishActions()
        {
        }
    }
}