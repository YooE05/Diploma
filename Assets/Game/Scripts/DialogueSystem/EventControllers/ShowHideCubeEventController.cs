using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class ShowHideCubeEventController : DialogueEventController
    {
        private readonly CubeHandler _cubeHandler;

        public ShowHideCubeEventController(CubeHandler cubeHandler, DialogueState dialogueState,
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