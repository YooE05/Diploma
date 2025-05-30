using System.Collections.Generic;
using Game.Tutorial.Gameplay;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class Stage3TaskTracker
    {
        private bool _separatePlantChecked;
        private bool _gardenChecked;

        private readonly CharactersDataHandler _charactersDataHandler;
        private readonly TaskPanel _taskPanel;
        private readonly NavigationManager _navigationManager;

        private const string CheckGardenText = "Осмотреть грядку";
        private const string CheckSeparatePlantText = "Убрать семечко";

        public Stage3TaskTracker(CharactersDataHandler charactersDataHandler, TaskPanel taskPanel,
            NavigationManager navigationManager)
        {
            _taskPanel = taskPanel;
            _charactersDataHandler = charactersDataHandler;
            _navigationManager = navigationManager;

            ResetTasks();
        }

        private bool IsAllTasksComplete => _separatePlantChecked && _gardenChecked;

        public void CheckGarden()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(CheckGardenText);
            _gardenChecked = true;

            if (!_separatePlantChecked)
                _navigationManager.SetNavigationToSeed();

            CheckTaskCompletion();
        }

        public void CheckSeparatePlant()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(CheckSeparatePlantText);
            _separatePlantChecked = true;

            if (!_gardenChecked)
                _navigationManager.SetNavigationToGarden();

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

            _navigationManager.SetNavigationToMainNpc();
        }

        public void ShowTasksText()
        {
            _taskPanel.SetUpTasks(new List<string> { CheckGardenText, CheckSeparatePlantText });
            _taskPanel.Show();
        }
    }
}