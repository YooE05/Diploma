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
            ResetTasks();
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

    public sealed class Stage4TaskTracker
    {
        private bool _seedsPlanted;
        private bool _lightEnabled;

        private readonly CharactersDataHandler _charactersDataHandler;

        public Stage4TaskTracker(CharactersDataHandler charactersDataHandler)
        {
            _charactersDataHandler = charactersDataHandler;
            ResetTasks();
        }

        private bool IsAllTasksComplete => _seedsPlanted && _lightEnabled;

        public void AdjustGardenLight()
        {
            if (IsAllTasksComplete) return;

            _lightEnabled = true;
            CheckTaskCompletion();
        }

        public void PlantSeeds()
        {
            if (IsAllTasksComplete) return;

            _seedsPlanted = true;
            CheckTaskCompletion();
        }

        public void ResetTasks()
        {
            _seedsPlanted = false;
            _lightEnabled = false;
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