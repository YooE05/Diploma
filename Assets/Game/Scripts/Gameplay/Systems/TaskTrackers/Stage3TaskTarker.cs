using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class Stage3TaskTracker
    {
        private bool _separatePlantChecked;
        private bool _gardenChecked;

        private readonly CharactersDataHandler _charactersDataHandler;

        public Stage3TaskTracker(CharactersDataHandler charactersDataHandler)
        {
            _charactersDataHandler = charactersDataHandler;
        }

        private bool IsAllTasksComplete => _separatePlantChecked && _gardenChecked;

        public void CheckGarden()
        {
            if (IsAllTasksComplete) return;

            _gardenChecked = true;
            CheckTaskCompletion();
        }

        public void CheckSeparatePlant()
        {
            if (IsAllTasksComplete) return;

            _separatePlantChecked = true;
            CheckTaskCompletion();
        }

        public void ResetTasks()
        {
            _separatePlantChecked = false;
            _gardenChecked = false;
            //Show tasks in UI
        }

        private void CheckTaskCompletion()
        {
            if (!IsAllTasksComplete) return;

            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);
        }
    }
}