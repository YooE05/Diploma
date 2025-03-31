using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage5WaitMeasureStickInitializer : StageInitializer
    {
        [SerializeField] private Stage5StartInitializer _startInitializer;
        [Inject] private Stage5TaskTracker _taskTracker;
        [Inject] private LockersViewController _lockersViewController;

        public override void InitGameView()
        {
            _startInitializer.InitGameView();
            _taskTracker.ShowTasksText();
            _lockersViewController.EnableLockersInteraction();
        }
    }
}