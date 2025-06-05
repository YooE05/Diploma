using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public class ShooterGameLoopController : MonoBehaviour
    {
        private LifecycleManager _lifecycleManager;
        private EnemyWaveObserver _enemyWaveObserver;
        private PlayerShooterBrain _playerBrain;
        private UpdateTimer _timer;
        private SaveLoadManager _saveLoadManager;
        private AudioManager _audioManager;
        private CharactersDataHandler _charactersDataHandler;
        [SerializeField] private ShooterEndZone _shooterEndZone;

        [SerializeField] private AudioClip _audioClip;

        [Inject] private LoadingScreen _loadingScreen;

        [Inject]
        public void Construct(LifecycleManager lifecycleManager, EnemyWaveObserver enemyWaveObserver,
            PlayerShooterBrain playerBrain, UpdateTimer timer, SaveLoadManager saveLoadManager,
            AudioManager audioManager, CharactersDataHandler charactersDataHandler)
        {
            _charactersDataHandler = charactersDataHandler;
            _audioManager = audioManager;
            _lifecycleManager = lifecycleManager;
            _enemyWaveObserver = enemyWaveObserver;
            _playerBrain = playerBrain;
            _timer = timer;

            _saveLoadManager = saveLoadManager;
            _enemyWaveObserver.OnAllEnemiesDead += FinishGame;
            if (_shooterEndZone != null) _shooterEndZone.OnFinish += FinishGame;
        }

        private void Start()
        {
            _loadingScreen.Show();
            _saveLoadManager.OnDataLoaded += StartGameplay;
            _saveLoadManager.LoadGame();
        }

        private void StartGameplay()
        {
            _saveLoadManager.OnDataLoaded -= StartGameplay;

            if (_audioManager.TryGetAudioClipByName("battleTheme", out var audioClip))
            {
                _audioManager.PlaySound(audioClip, AudioOutput.Music);
            }

            _lifecycleManager.OnStart();
            _timer.RestartTimer();
            _loadingScreen.Hide();
        }

        private void FinishGame()
        {
            _enemyWaveObserver.OnAllEnemiesDead -= FinishGame;
            if (_shooterEndZone != null) _shooterEndZone.OnFinish -= FinishGame;
            if (_playerBrain.IsDead) return;

            _timer.StopTimer();
            _lifecycleManager.OnFinish();
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _saveLoadManager.SaveGame();
        }

        //TODO: replace reload scene by reInitialization all systems to better performance

        public void RetryGameLoop()
        {
            _loadingScreen.Show();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _sceneHandle = Addressables.LoadSceneAsync("ScienceBaseVisual", LoadSceneMode.Single);
        }

        private AsyncOperationHandle<SceneInstance> _sceneHandle;
        
        public void GoNextLevel()
        {
            Time.timeScale = 1f;
            _sceneHandle = Addressables.LoadSceneAsync("ScienceBaseVisual", LoadSceneMode.Single);
          //  SceneManager.LoadScene("ScienceBaseVisual"); //(_nextSceneName);
        }

        [Button]
        private void ResetGame()
        {
            _saveLoadManager.ResetGame();
            RetryGameLoop();
        }
    }
}