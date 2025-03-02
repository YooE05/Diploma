using System.Collections.Generic;
using DS.ScriptableObjects;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class ShowHideCubeEventController : DialogueEventController
    {
        private readonly CubeController _cubeController;

        public ShowHideCubeEventController(CubeController cubeController, DialogueState dialogueState,
            List<DSDialogueSO> dialogues) :
            base(dialogueState, dialogues)
        {
            _cubeController = cubeController;
        }

        protected override void StartActions()
        {
            _cubeController.EnableCube();
        }

        protected override void FinishActions()
        {
            _cubeController.DisableCube();
        }
    }
}