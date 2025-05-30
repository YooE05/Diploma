using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class Stage6ShowAverageNotesEvent : DialogueEvent
    {
        private readonly AverageNotesAndConclusionUI _averageNotesUI;

        public Stage6ShowAverageNotesEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            AverageNotesAndConclusionUI averageNotesUI) :
            base(dialogueState, dialogues)
        {
            _averageNotesUI = averageNotesUI;
        }

        protected override void StartActions()
        {
            _averageNotesUI.ShowAverageNotes();
        }
    }

    public sealed class Stage6HideAverageNotesEvent : DialogueEvent
    {
        private readonly AverageNotesAndConclusionUI _averageNotesUI;

        public Stage6HideAverageNotesEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            AverageNotesAndConclusionUI averageNotesUI) :
            base(dialogueState, dialogues)
        {
            _averageNotesUI = averageNotesUI;
        }

        protected override void FinishActions()
        {
            _averageNotesUI.HideAverageNotes();
        }
    }

    public sealed class Stage6ShowConclusionPopupEvent : DialogueEvent
    {
        private readonly AverageNotesAndConclusionUI _averageNotesUI;

        public Stage6ShowConclusionPopupEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            AverageNotesAndConclusionUI averageNotesUI) :
            base(dialogueState, dialogues)
        {
            _averageNotesUI = averageNotesUI;
        }

        protected override void StartActions()
        {
            _averageNotesUI.ShowConclusionPopup();
        }
    }

    public sealed class Stage6HideConclusionPopupEvent : DialogueEvent
    {
        private readonly AverageNotesAndConclusionUI _averageNotesUI;

        public Stage6HideConclusionPopupEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            AverageNotesAndConclusionUI averageNotesUI) :
            base(dialogueState, dialogues)
        {
            _averageNotesUI = averageNotesUI;
        }

        protected override void FinishActions()
        {
            _averageNotesUI.HideConclusionPopup();
        }
    }

    public sealed class Stage6ShowGraphEvent : DialogueEvent
    {
        private readonly GraphPanelInteraction _graphPanel;

        public Stage6ShowGraphEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            GraphPanelInteraction graphPanel) :
            base(dialogueState, dialogues)
        {
            _graphPanel = graphPanel;
        }

        protected override void FinishActions()
        {
            _graphPanel.Show();
        }
    } 
    
    public sealed class Stage6HideLittleGraphEvent : DialogueEvent
    {
        private readonly GraphPanelInteraction _graphPanel;

        public Stage6HideLittleGraphEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            GraphPanelInteraction graphPanel) :
            base(dialogueState, dialogues)
        {
            _graphPanel = graphPanel;
        }

        protected override void StartActions()
        {
            _graphPanel.HideLittleGraph();
        }
    }
}