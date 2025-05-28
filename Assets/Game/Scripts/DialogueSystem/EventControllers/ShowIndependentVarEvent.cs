using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class ShowIndependentVarEvent : DialogueEvent
    {
        private readonly VariablesPopup _variablesPopup;

        public ShowIndependentVarEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            VariablesPopup variablesPopup) :
            base(dialogueState, dialogues)
        {
            _variablesPopup = variablesPopup;
        }

        protected override void StartActions()
        {
            _variablesPopup.ShowIndependentPopup();
        }
    }

    public sealed class ShowDependentVarEvent : DialogueEvent
    {
        private readonly VariablesPopup _variablesPopup;

        public ShowDependentVarEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            VariablesPopup variablesPopup) :
            base(dialogueState, dialogues)
        {
            _variablesPopup = variablesPopup;
        }

        protected override void StartActions()
        {
            _variablesPopup.ShowDependentPopup();
        }
    }

    public sealed class HideVariablesEvent : DialogueEvent
    {
        private readonly VariablesPopup _variablesPopup;

        public HideVariablesEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            VariablesPopup variablesPopup) :
            base(dialogueState, dialogues)
        {
            _variablesPopup = variablesPopup;
        }

        protected override void FinishActions()
        {
            _variablesPopup.Hide();
        }
    }

    public sealed class ShowQualityDataEvent : DialogueEvent
    {
        private readonly DataPopup _dataPopup;

        public ShowQualityDataEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            DataPopup dataPopup) :
            base(dialogueState, dialogues)
        {
            _dataPopup = dataPopup;
        }

        protected override void StartActions()
        {
            _dataPopup.ShowQualityPopup();
        }
    }

    public sealed class ShowQuantitativeDataEvent : DialogueEvent
    {
        private readonly DataPopup _dataPopup;

        public ShowQuantitativeDataEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            DataPopup dataPopup) :
            base(dialogueState, dialogues)
        {
            _dataPopup = dataPopup;
        }

        protected override void StartActions()
        {
            _dataPopup.ShowQuantitativePopup();
        }
    }

    public sealed class HideDataEvent : DialogueEvent
    {
        private readonly DataPopup _dataPopup;

        public HideDataEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            DataPopup dataPopup) :
            base(dialogueState, dialogues)
        {
            _dataPopup = dataPopup;
        }

        protected override void StartActions()
        {
            _dataPopup.Hide();
        }
    }
}