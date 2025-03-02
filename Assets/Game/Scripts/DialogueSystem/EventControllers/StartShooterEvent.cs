using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

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
            _scienceBaseGameController.GoToShooterScene();
        }
    }
}