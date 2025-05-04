using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TaskPanel : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _panelGO;
        [SerializeField] private List<TaskView> _taskList;

        public void OnInit()
        {
            ClearTasks();
            Hide();
        }

        public void Show()
        {
            _panelGO.SetActive(true);
        }

        public void Hide()
        {
            _panelGO.SetActive(false);
        }

        public void SetUpTasks(List<string> tasks)
        {
            ClearTasks();

            for (var i = 0; i < tasks.Count; i++)
            {
                _taskList[i].Show(tasks[i]);
            }
        }

        public void CompleteTask(string completeTaskText)
        {
            for (var i = 0; i < _taskList.Count; i++)
            {
                _taskList[i].TryComplete(completeTaskText);
            }
        }

        private void ClearTasks()
        {
            for (var i = 0; i < _taskList.Count; i++)
            {
                _taskList[i].Hide();
            }
        }
    }
}