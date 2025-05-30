using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterPauseView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameView;
        [SerializeField] private ButtonView _pauseButton;

        [SerializeField] private ConfirmPopupView _confirmPopupView;

        private LoadingScreen _loadingScreen;
        private ShooterLoader _shooterLoader;

        [Inject]
        public void Construct(ShooterLoader shooterLoader, LoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
            _shooterLoader = shooterLoader;
        }

        private void Awake()
        {
            _confirmPopupView.HideNoAnimation();
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
            _loadingScreen.Show();
            _shooterLoader.UnloadShooterScene();

            Time.timeScale = 1f;
            SceneManager.LoadScene("ScienceBaseVisual");
        }
    }
}