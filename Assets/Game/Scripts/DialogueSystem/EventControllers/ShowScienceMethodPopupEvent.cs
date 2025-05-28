using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class ShowScienceMethodPopupEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public ShowScienceMethodPopupEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void StartActions()
        {
            _scienceMethodPopup.Show();
        }
    }

    public sealed class HideScienceMethodPopupEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public HideScienceMethodPopupEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void FinishActions()
        {
            _scienceMethodPopup.Hide();
        }
    }

    public sealed class EnableObservationEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public EnableObservationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void StartActions()
        {
            _scienceMethodPopup.Show();
            _scienceMethodPopup.EnableObservation();
        }
    }

    public sealed class EnableHypothesisEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public EnableHypothesisEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void StartActions()
        {
            _scienceMethodPopup.Show();
            _scienceMethodPopup.EnableHypothesis();
        }
    }

    public sealed class EnableExperimentEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public EnableExperimentEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void StartActions()
        {
            _scienceMethodPopup.Show();
            _scienceMethodPopup.EnableExperiment();
        }
    }

    public sealed class EnableAnalysisEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public EnableAnalysisEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void StartActions()
        {
            _scienceMethodPopup.Show();
            _scienceMethodPopup.EnableAnalysis();
        }
    }

    public sealed class EnableConclusionEvent : DialogueEvent
    {
        private readonly ScienceMethodPopup _scienceMethodPopup;

        public EnableConclusionEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            ScienceMethodPopup scienceMethodPopup) :
            base(dialogueState, dialogues)
        {
            _scienceMethodPopup = scienceMethodPopup;
        }

        protected override void StartActions()
        {
            _scienceMethodPopup.Show();
            _scienceMethodPopup.EnableConclusion();
        }
    }
}