using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterPauseView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameView;
        [SerializeField] private ButtonView _pauseButton;

        [SerializeField] private ConfirmPopupView _confirmPopupView;

        private ShooterGameLoopController _shooterGameLoopController;

        [Inject]
        public void Construct(ShooterGameLoopController shooterGameLoopController)
        {
            _shooterGameLoopController = shooterGameLoopController;
        }

        private void Awake()
        {
            _confirmPopupView.Hide();
        }

        private void OnEnable()
        {
            _pauseButton.OnButtonClicked += OpenPausePanel;
            _confirmPopupView.OnConfirm += ExitLevel;
            _confirmPopupView.OnDecline += HidePausePanel;
        }

        private void OnDisable()
        {
            _pauseButton.OnButtonClicked -= OpenPausePanel;
            _confirmPopupView.OnConfirm -= ExitLevel;
            _confirmPopupView.OnDecline -= HidePausePanel;
        }

        private void OpenPausePanel()
        {
            _gameView.SetActive(false);
            _confirmPopupView.Show();
            Time.timeScale = 0f;
        }

        private void HidePausePanel()
        {
            _gameView.SetActive(true);
            _confirmPopupView.Hide();
            Time.timeScale = 1f;
        }

        private void ExitLevel()
        {
            Time.timeScale = 1f;
            _shooterGameLoopController.GoNextLevel();
        }
    }
}