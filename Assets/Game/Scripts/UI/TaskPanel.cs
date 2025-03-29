using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TaskPanel : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _panelGO;
        [SerializeField] private List<TextMeshProUGUI> _taskList = new();

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
            for (var i = 0; i < _taskList.Count; i++)
            {
                _taskList[i].text = i + 1 <= tasks.Count ? tasks[i] : string.Empty;
            }
        }

        public void CompleteTask(string completeTaskText)
        {
            var tmpCompleted = _taskList.Find(t => t.text == completeTaskText);
            tmpCompleted.fontStyle = FontStyles.Strikethrough;
        }

        private void ClearTasks()
        {
            for (var i = 0; i < _taskList.Count; i++)
            {
                _taskList[i].text = string.Empty;
            }
        }
    }
}