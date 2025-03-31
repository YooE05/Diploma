using System.Collections.Generic;
using YooE.DialogueSystem;

namespace YooE.Diploma
{
    public sealed class Stage4TaskTracker
    {
        private bool _seedsPlanted;
        private bool _lightEnabled;

        private readonly CharactersDataHandler _charactersDataHandler;
        private readonly TaskPanel _taskPanel;

        private const string AdjustGardenLightText = "Настроить свет";
        private const string PlantSeedsText = "Посадить семена";

        public Stage4TaskTracker(CharactersDataHandler charactersDataHandler, TaskPanel taskPanel)
        {
            _taskPanel = taskPanel;
            _charactersDataHandler = charactersDataHandler;

            _taskPanel.Hide();
            ResetTasks();
        }

        private bool IsAllTasksComplete => _seedsPlanted && _lightEnabled;

        public void AdjustGardenLight()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(AdjustGardenLightText);
            _lightEnabled = true;
            CheckTaskCompletion();
        }

        public void PlantSeeds()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(PlantSeedsText);
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

        public void ShowTasksText()
        {
            _taskPanel.SetUpTasks(new List<string> { AdjustGardenLightText, PlantSeedsText });
            _taskPanel.Show();
        }
    }
}