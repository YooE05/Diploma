using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

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
        [SerializeField] private TextMeshProUGUI _newRecordText;

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
            _retryButtonPopup.transform.DOScale(0f, 0f).SetLink(_retryButtonPopup).Play();
            _retryButtonPopup.SetActive(true);
            _retryButtonPopup.transform.DOScale(1f, 0.6f).SetLink(_retryButtonPopup).Play();

            _fadeImage.DOFade(0f, 0f).SetLink(_fadeImage.gameObject).Play();
            _fadeImage.DOFade(0.72f, 0.5f).SetLink(_fadeImage.gameObject).Play();
            _fadeImage.enabled = true;
        }

        public void SetupEndPopup(string enemyDefeatPercent, string spentTime)
        {
            _enemyStatisticText.text = enemyDefeatPercent;
            _timeStatisticText.text = spentTime;

            _fadeImage.DOFade(0f, 0f).SetLink(_fadeImage.gameObject).Play();
            _fadeImage.DOFade(0.72f, 0.5f).SetLink(_fadeImage.gameObject).Play();
            _fadeImage.enabled = true;

            _endPopup.transform.DOScale(0f, 0f).SetLink(_endPopup).Play();
            _endPopup.SetActive(true);
            _endPopup.transform.DOScale(1f, 0.6f).SetLink(_endPopup).Play();
        }

        public void SetupNewRecordText(string recordText)
        {
            _newRecordText.text = recordText;
            _newRecordText.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _newRecordText.gameObject.SetActive(false);
            _fadeImage.enabled = false;
            _retryButtonPopup.SetActive(false);
            _endPopup.SetActive(false);
        }
    }

    public sealed class ShooterPopupsPresenter : Listeners.IFinishListener
    {
        private readonly ShooterPopupsView _shooterPopupsView;

        // private readonly ShooterGameLoopController _shooterGameLoopController;
        private readonly EnemiesInitializer _enemiesInitializer;
        private readonly UpdateTimer _timer;
        private readonly bool _isEndless;
        private readonly ShooterLoader _shooterLoader;

        public ShooterPopupsPresenter(ShooterPopupsView shooterPopupsView,
            ShooterLoader shooterLoader,
//            ShooterGameLoopController shooterGameLoopController,
            EnemiesInitializer enemiesInitializer, UpdateTimer timer, bool isEndless = false)
        {
            _isEndless = isEndless;
            _shooterLoader = shooterLoader;

            _shooterPopupsView = shooterPopupsView;
            _enemiesInitializer = enemiesInitializer;
            //   _shooterGameLoopController = shooterGameLoopController;
            _timer = timer;

            InitPopupsView();

            _shooterPopupsView.OnRetryButtonClicked += RetryGame;
            _shooterPopupsView.OnContinueButtonClicked += GoNextLevel;
        }

        private void GoNextLevel()
        {
            _shooterLoader.UnloadShooterScene();
            //_shooterGameLoopController.GoNextLevel();
            Time.timeScale = 1f;
            SceneManager.LoadScene("ScienceBaseVisual");
        }

        private void RetryGame()
        {
            //_shooterGameLoopController.RetryGameLoop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            if (!_isEndless)
                ShowEndGamePopup();
        }

        public void ShowRetryPanel()
        {
            _shooterPopupsView.ShowRetryButton();
        }

        public void ShowEndGamePopup()
        {
            string stringDefeat;
            if (_isEndless)
            {
                stringDefeat = (_enemiesInitializer.GetTotalDefeatCount()).ToString();

                var recordText = "НОВЫЙ РЕКОРД!";

                var currentScore = _enemiesInitializer.GetTotalDefeatCount();
                var recordScore = _enemiesInitializer.BestScore;
                if (currentScore <= recordScore)
                    recordText = $"РЕКОРД - {recordScore}";

                _shooterPopupsView.SetupNewRecordText(recordText);
            }
            else
            {
                stringDefeat = ((int)_enemiesInitializer.GetDefeatPercent()).ToString();
                stringDefeat = $"{stringDefeat} %";
            }

            var time = new TimeSpan(0, 0, (int)_timer.CurrentTime);
            var stringSpentTime = $"{time.Minutes}:";
            stringSpentTime += time.Seconds < 10 ? $"0{time.Seconds}" : $"{time.Seconds}";
            _shooterPopupsView.SetupEndPopup(stringDefeat, stringSpentTime);
        }
    }
}