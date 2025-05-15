using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YooE.Diploma
{
    public sealed class ShooterPopupsView : MonoBehaviour
    {
        public event Action OnRetryButtonClicked;
        public event Action OnContinueButtonClicked;

        [SerializeField] private ButtonView _retryButton;
        [SerializeField] private GameObject _retryButtonPopup;

        [SerializeField] private GameObject _endPopup;
        [SerializeField] private ButtonView _continueButton;
        [SerializeField] private TextMeshProUGUI _enemyStatisticText;
        [SerializeField] private TextMeshProUGUI _timeStatisticText;

        [SerializeField] private Image _fadeImage;

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
            _retryButtonPopup.SetActive(true);
            _fadeImage.enabled = true;
        }

        public void SetupEndPopup(string enemyDefeatPercent, string spentTime)
        {
            _enemyStatisticText.text = $"{enemyDefeatPercent} %";
            _timeStatisticText.text = spentTime;
            _fadeImage.enabled = true;
            _endPopup.SetActive(true);
        }

        public void Hide()
        {
            _fadeImage.enabled = false;
            _retryButtonPopup.SetActive(false);
            _endPopup.SetActive(false);
        }
    }

    public sealed class ShooterPopupsPresenter : Listeners.IFinishListener
    {
        private readonly ShooterPopupsView _shooterPopupsView;
        private readonly ShooterGameLoopController _shooterGameLoopController;
        private readonly EnemiesInitializer _enemiesInitializer;
        private readonly UpdateTimer _timer;

        public ShooterPopupsPresenter(ShooterPopupsView shooterPopupsView,
            ShooterGameLoopController shooterGameLoopController,
            EnemiesInitializer enemiesInitializer, UpdateTimer timer)
        {
            _shooterPopupsView = shooterPopupsView;
            _enemiesInitializer = enemiesInitializer;
            _shooterGameLoopController = shooterGameLoopController;
            _timer = timer;

            InitPopupsView();

            _shooterPopupsView.OnRetryButtonClicked += RetryGame;
            _shooterPopupsView.OnContinueButtonClicked += GoNextLevel;
        }

        private void GoNextLevel()
        {
            _shooterGameLoopController.GoNextLevel();
        }

        private void RetryGame()
        {
            _shooterGameLoopController.RetryGameLoop();
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
            var stringDefeatPercent = ((int)_enemiesInitializer.GetDefeatPercent()).ToString();
            var time = new TimeSpan(0, 0, (int)_timer.CurrentTime);
            var stringSpentTime = $"{time.Minutes}:";
            stringSpentTime += time.Seconds < 10 ? $"0{time.Seconds}" : $"{time.Seconds}";
            _shooterPopupsView.SetupEndPopup(stringDefeatPercent, stringSpentTime);
        }
    }
}