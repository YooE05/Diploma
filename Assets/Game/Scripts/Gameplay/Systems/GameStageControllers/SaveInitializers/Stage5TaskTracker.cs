using System.Collections.Generic;
using Game.Tutorial.Gameplay;
using YooE.DialogueSystem;
using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class Stage5TaskTracker
    {
        private bool _notepadPicked;
        private bool _measureStickPicked;

        private bool _isPlantsMeasured;

        private readonly LockersViewController _lockersController;
        private readonly CharactersDataHandler _charactersDataHandler;
        private readonly TaskPanel _taskPanel;
        private readonly SaveLoadManager _saveLoadManager;
        private readonly NavigationManager _navigationManager;

        private const string FindMeasureStick = "Найти линейку";
        private const string FindNotepad = "Найти блокнот";

        private const string MeasurePlantsText = "Измерить растения";

        public Stage5TaskTracker(CharactersDataHandler charactersDataHandler, TaskPanel taskPanel,
            LockersViewController lockersController, SaveLoadManager saveLoadManager,
            NavigationManager navigationManager)
        {
            _taskPanel = taskPanel;
            _charactersDataHandler = charactersDataHandler;
            _lockersController = lockersController;
            _saveLoadManager = saveLoadManager;
            _navigationManager = navigationManager;

            _taskPanel.Hide();
            ResetTasks();
        }

        private bool IsAllTasksComplete => _notepadPicked && _measureStickPicked;

        public void PickupMeasureStick()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(FindMeasureStick);
            _measureStickPicked = true;
            CheckTaskCompletion();
        }

        public void PickupNotepad()
        {
            if (IsAllTasksComplete) return;

            _taskPanel.CompleteTask(FindNotepad);
            _notepadPicked = true;
            CheckTaskCompletion();
        }

        public void MeasurePlants()
        {
            _taskPanel.CompleteTask(MeasurePlantsText);
            _notepadPicked = true;
        }

        public void ResetTasks()
        {
            _notepadPicked = false;
            _measureStickPicked = false;
        }

        private void CheckTaskCompletion()
        {
            if (!IsAllTasksComplete) return;

            _lockersController.DisableLockersInteraction();

            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _charactersDataHandler.UpdateCharacterDialogueIndex(DialogueCharacterID.MainScientist);

            _navigationManager.SetNavigationToMainNpc();

            _saveLoadManager.SaveGame();
        }

        public void ShowTasksText()
        {
            _taskPanel.SetUpTasks(new List<string> { FindMeasureStick, FindNotepad });
            _taskPanel.Show();
        }

        public void ShowMeasurePlantsTaskText()
        {
            _taskPanel.SetUpTasks(new List<string> { MeasurePlantsText });
            _taskPanel.Show();
        }

        public void ShowCompletedTasks()
        {
            ShowTasksText();

            _taskPanel.CompleteTask(FindMeasureStick);
            _measureStickPicked = true;

            _taskPanel.CompleteTask(FindNotepad);
            _notepadPicked = true;
        }
    }
}