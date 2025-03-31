using System.Collections.Generic;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class Stage3TaskTracker
    {
        private bool _separatePlantChecked;
        private bool _gardenChecked;

        private readonly CharactersDataHandler _charactersDataHandler;
        private readonly TaskPanel _taskPanel;

        private const string CheckGardenText = "Осмотреть грядку";
        private const string CheckSeparatePlantText = "Подобрать упавшее семечко";

        public Stage3TaskTracker(CharactersDataHandler charactersDataHandler, TaskPanel taskPanel)
        {
            _taskPanel = taskPanel;
            _charactersDataHandler = charactersDataHandler;
            ResetTasks();
        }

        private bool IsAllTasksComplete => _separatePlantChecked && _gardenChecked;

        public void CheckGarden()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(CheckGardenText);
            _gardenChecked = true;
            CheckTaskCompletion();
        }

        public void CheckSeparatePlant()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(CheckSeparatePlantText);
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

        public void ShowTasksText()
        {
            _taskPanel.SetUpTasks(new List<string> { CheckGardenText, CheckSeparatePlantText });
            _taskPanel.Show();
        }
    }
}