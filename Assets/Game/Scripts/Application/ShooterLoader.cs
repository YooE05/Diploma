using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace YooE
{
    public sealed class ShooterLoader
    {
        private string _currentScene;

        private AsyncOperationHandle<SceneInstance> _sceneHandle;

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
                0 => "ShooterTutorialEasy",
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

            _currentScene = assetKey;
         //   const bool activateOnLoad = false;

            _sceneHandle = Addressables.LoadSceneAsync(_currentScene, LoadSceneMode.Single);
                // _sceneHandle.Completed += delegate { _sceneHandle.Result.ActivateAsync(); };
        }

        public void ReloadScene()
        {
            _sceneHandle = Addressables.LoadSceneAsync(_currentScene, LoadSceneMode.Single);
         //   _sceneHandle.Completed += delegate { _sceneHandle.Result.ActivateAsync(); };
        }
    }
}