using System;
using TMPro;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class ShooterPopupsView : MonoBehaviour
    {
        public event Action OnRetryButtonClicked;
        public event Action OnContinueButtonClicked;

        [SerializeField] private ButtonView _retryButton;

        [SerializeField] private GameObject _endPopup;
        [SerializeField] private ButtonView _continueButton;
        [SerializeField] private TextMeshProUGUI _enemyStatisticText;
        [SerializeField] private TextMeshProUGUI _timeStatisticText;

        private void OnEnable()
        {
            _retryButton.OnButtonClicked += RetryButtonClicked;
            _continueButton.OnButtonClicked += ContinueButtonClicked;
        }

        private void ContinueButtonClicked()
        {
            OnContinueButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            _retryButton.OnButtonClicked -= RetryButtonClicked;
            _continueButton.OnButtonClicked -= ContinueButtonClicked;
        }

        private void RetryButtonClicked()
        {
            OnRetryButtonClicked?.Invoke();
        }

        public void ShowRetryButton()
        {
            _retryButton.Show();
        }

        public void SetupEndPopup(string enemyDefeatPercent, string spentTime)
        {
            _enemyStatisticText.text = enemyDefeatPercent;
            _timeStatisticText.text = spentTime;
            _endPopup.SetActive(true);
        }

        public void Hide()
        {
            _retryButton.Hide();
            _endPopup.SetActive(false);
        }
    }

    public sealed class ShooterPopupsPresenter : Listeners.IFinishListener
    {
        private readonly ShooterPopupsView _shooterPopupsView;
        private readonly GameLoopController _gameLoopController;
        private readonly EnemyBrainsInitializer _enemyBrainsInitializer;
        private readonly UpdateTimer _timer;

        public ShooterPopupsPresenter(ShooterPopupsView shooterPopupsView, GameLoopController gameLoopController,
            EnemyBrainsInitializer enemyBrainsInitializer, UpdateTimer timer)
        {
            _shooterPopupsView = shooterPopupsView;
            _enemyBrainsInitializer = enemyBrainsInitializer;
            _gameLoopController = gameLoopController;
            _timer = timer;

            InitPopupsView();

            _shooterPopupsView.OnRetryButtonClicked += RetryGame;
            _shooterPopupsView.OnContinueButtonClicked += GoNextLevel;
        }

        private void GoNextLevel()
        {
            _gameLoopController.GoNextLevel();
        }

        private void RetryGame()
        {
            _gameLoopController.RetryGameLoop();
        }

        private void InitPopupsView()
        {
            _shooterPopupsView.Hide();
        }

        ~ShooterPopupsPresenter()
        {
            _shooterPopupsView.OnRetryButtonClicked -= RetryGame;
        }

        public void OnFinish()
        {
            ShowEndGamePopup();
        }

        public void ShowRetryPanel()
        {
            _shooterPopupsView.ShowRetryButton();
        }

        public void ShowEndGamePopup()
        {
            var stringDefeatPercent = ((int)_enemyBrainsInitializer.GetDefeatPercent()).ToString();
            var time = new TimeSpan(0, 0, (int)_timer.CurrentTime);
            var stringSpentTime = $"{time.Minutes} : {time.Seconds}";
            _shooterPopupsView.SetupEndPopup(stringDefeatPercent, stringSpentTime);
        }
    }
}