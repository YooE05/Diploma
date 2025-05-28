using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class ShowHideCubeEvent : DialogueEvent
    {
        private readonly CubeHandler _cubeHandler;

        public ShowHideCubeEvent(CubeHandler cubeHandler, DialogueState dialogueState,
            List<DSDialogueSO> dialogues) :
            base(dialogueState, dialogues)
        {
            _cubeHandler = cubeHandler;
        }

        protected override void StartActions()
        {
            _cubeHandler.EnableCube();
        }

        protected override void FinishActions()
        {
            _cubeHandler.DisableCube();
        }
    }
}