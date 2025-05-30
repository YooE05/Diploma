using System.Collections.Generic;
using DS.ScriptableObjects;
using Game.Tutorial.Gameplay;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class HideNavigationEvent : DialogueEvent
    {
        private readonly NavigationManager _navigationManager;

        public HideNavigationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            NavigationManager navigationManager) :
            base(dialogueState, dialogues)
        {
            _navigationManager = navigationManager;
        }

        protected override void StartActions()
        {
            _navigationManager.Stop();
        }
    }   
    
    public sealed class SetNPCNavigationEvent : DialogueEvent
    {
        private readonly NavigationManager _navigationManager;

        public SetNPCNavigationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            NavigationManager navigationManager) :
            base(dialogueState, dialogues)
        {
            _navigationManager = navigationManager;
        }

        protected override void FinishActions()
        {
            _navigationManager.SetNavigationToMainNpc();
        }
    } 
    
    public sealed class SetDoorNavigationEvent : DialogueEvent
    {
        private readonly NavigationManager _navigationManager;

        public SetDoorNavigationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            NavigationManager navigationManager) :
            base(dialogueState, dialogues)
        {
            _navigationManager = navigationManager;
        }

        protected override void FinishActions()
        {
            _navigationManager.SetNavigationToDoor();
        }
    }   
    
    public sealed class SetSeedNavigationEvent : DialogueEvent
    {
        private readonly NavigationManager _navigationManager;

        public SetSeedNavigationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            NavigationManager navigationManager) :
            base(dialogueState, dialogues)
        {
            _navigationManager = navigationManager;
        }

        protected override void FinishActions()
        {
            _navigationManager.SetNavigationToGarden();
        }
    }
    
    public sealed class SetLeverNavigationEvent : DialogueEvent
    {
        private readonly NavigationManager _navigationManager;

        public SetLeverNavigationEvent(DialogueState dialogueState, List<DSDialogueSO> dialogues,
            NavigationManager navigationManager) :
            base(dialogueState, dialogues)
        {
            _navigationManager = navigationManager;
        }

        protected override void FinishActions()
        {
            _navigationManager.SetNavigationToLever();
        }
    }
    
}