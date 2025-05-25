using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace YooE
{
    public sealed class ShooterLoader
    {
        /*private readonly MenuLoader _menuLoader;*/
        // private const string SceneKey = "GameScene";

        private AsyncOperationHandle<SceneInstance> _sceneHandle;

        /*
        public GameLoader(MenuLoader menuLoader)
        {
            _menuLoader = menuLoader;
            _menuLoader.OnBackToMenu += UnloadGame;
        }*/

        public void UnloadShooterScene()
        {
            if (!_sceneHandle.IsValid()) return;
            
            if (!_sceneHandle.Result.Scene.isLoaded)
            {
                return;
            }

            Addressables.UnloadSceneAsync(_sceneHandle, UnloadSceneOptions.None);
        }

        public void LoadShooterScene(int mainDialogueGroupIndex)
        {
            var sceneName = mainDialogueGroupIndex switch
            {
                0 => "ShooterTutorial",
                2 => "Shooter1",
                6 => "Shooter2",
                10 => "Shooter3",
                17 => "Shooter4",
                19 => "Shooter5",
                _ => "ShooterEndless"
            };

            InitSceneAsset(sceneName);
            //  SceneManager.LoadScene(sceneName);
        }

        private void InitSceneAsset(string assetKey)
        {
            if (_sceneHandle.IsValid() && _sceneHandle.IsDone)
            {
                return;
            }

            const bool activateOnLoad = false;

            _sceneHandle = Addressables.LoadSceneAsync(assetKey, LoadSceneMode.Single, activateOnLoad);
            _sceneHandle.Completed += delegate { _sceneHandle.Result.ActivateAsync(); };
        }
    }
}