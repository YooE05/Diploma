using UnityEditor;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ExitGamePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _gameViewFade;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private ButtonView _openSettingsButton;
        [SerializeField] private ButtonView _hideSettingsButton;

        [SerializeField] private ButtonView _showExitPopupButton;
        [SerializeField] private ConfirmPopupView _exitConfirmPopupView;

        [SerializeField] private ButtonView _showRestartGamePopupButton;
        [SerializeField] private ConfirmPopupView _restartConfirmPopupView;

        [Inject] private ScienceBaseGameController _scienceBaseGameController;

        private void Awake()
        {
            _gameViewFade.SetActive(false);
            _settingsPanel.SetActive(false);
            _exitConfirmPopupView.HideNoAnimation();
            _restartConfirmPopupView.HideNoAnimation();
        }

        private void OnEnable()
        {
            _openSettingsButton.OnButtonClicked += OpenSettingsPanel;
            _hideSettingsButton.OnButtonClicked += HideSettingsPanel;

            _showExitPopupButton.OnButtonClicked += ShowExitPanel;
            _exitConfirmPopupView.OnConfirm += ExitGame;
            _exitConfirmPopupView.OnDecline += HideExitPanel;

            _showRestartGamePopupButton.OnButtonClicked += ShowRestartPanel;
            _restartConfirmPopupView.OnConfirm += RestartGame;
            _restartConfirmPopupView.OnDecline += HideRestartPanel;
        }

        private void OnDisable()
        {
            _openSettingsButton.OnButtonClicked -= OpenSettingsPanel;
            _hideSettingsButton.OnButtonClicked -= HideSettingsPanel;

            _showExitPopupButton.OnButtonClicked -= ShowExitPanel;
            _exitConfirmPopupView.OnConfirm -= ExitGame;
            _exitConfirmPopupView.OnDecline -= HideExitPanel;

            _showRestartGamePopupButton.OnButtonClicked -= ShowRestartPanel;
            _restartConfirmPopupView.OnConfirm -= RestartGame;
            _restartConfirmPopupView.OnDecline -= HideRestartPanel;
        }

        private void OpenSettingsPanel()
        {
            Time.timeScale = 0f;
            _gameViewFade.SetActive(true);
            _settingsPanel.SetActive(true);
        }

        private void HideSettingsPanel()
        {
            Time.timeScale = 1f;
            _gameViewFade.SetActive(false);
            _settingsPanel.SetActive(false);
        }

        private void ShowExitPanel()
        {
            _settingsPanel.SetActive(false);
            _exitConfirmPopupView.Show();
        }

        private void HideExitPanel()
        {
            _settingsPanel.SetActive(true);
            _exitConfirmPopupView.Hide();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
            Application.Quit();
#elif UNITY_WEBGL
            // WebGL нельзя программно закрыть вкладку браузера без пользовательского действия,
            // поэтому либо перенаправим на другую страницу, либо покажем уведомление.
            // Здесь вызываем внешнюю JS-функцию:
            CloseBrowserWindow();
#else
            Application.Quit();
#endif
        }

        private void ShowRestartPanel()
        {
            _settingsPanel.SetActive(false);
            _restartConfirmPopupView.Show();
        }

        private void HideRestartPanel()
        {
            _settingsPanel.SetActive(true);
            _restartConfirmPopupView.Hide();
        }

        private void RestartGame()
        {
            Time.timeScale = 1f;
            _scienceBaseGameController.ResetGame();
        }

        /*
         Добавить для WebGL
         <script>
        function CloseBrowserWindow() {
            alert("Нажмите «Закрыть», чтобы закрыть окно.");
            window.close();
        }
        </script>*/
    }
}