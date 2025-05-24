using UnityEditor;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class ExitGamePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _gameViewFade;
        [SerializeField] private ButtonView _homeButton;

        [SerializeField] private ConfirmPopupView _confirmPopupView;

        private void Awake()
        {
            _gameViewFade.SetActive(false);
            _confirmPopupView.HideNoAnimation();
        }

        private void OnEnable()
        {
            _homeButton.OnButtonClicked += OpenExitPanel;
            _confirmPopupView.OnConfirm += ExitGame;
            _confirmPopupView.OnDecline += HideExitPanel;
        }

        private void OnDisable()
        {
            _homeButton.OnButtonClicked -= OpenExitPanel;
            _confirmPopupView.OnConfirm -= ExitGame;
            _confirmPopupView.OnDecline -= HideExitPanel;
        }

        private void OpenExitPanel()
        {
            Time.timeScale = 0f;
            _gameViewFade.SetActive(true);
            _confirmPopupView.Show();
        }

        private void HideExitPanel()
        {
            Time.timeScale = 1f;
            _gameViewFade.SetActive(false);
            _confirmPopupView.Hide();
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