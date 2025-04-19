using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterGameLoopController : MonoBehaviour
    {
        private LifecycleManager _lifecycleManager;
        private EnemyWaveObserver _enemyWaveObserver;
        private PlayerShooterBrain _playerBrain;
        private UpdateTimer _timer;
        private SaveLoadManager _saveLoadManager;
        private AudioManager _audioManager;
        private CharactersDataHandler _charactersDataHandler;

        [SerializeField] private string _nextSceneName;
        [SerializeField] private AudioClip _audioClip;

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
        }

        private void Start()
        {
            _saveLoadManager.OnDataLoaded += StartGameplay;
            _saveLoadManager.LoadGame();
        }

        private void StartGameplay()
        {
            _saveLoadManager.OnDataLoaded -= StartGameplay;

            _audioManager.PlaySound(_audioClip, AudioOutput.Music);
            _lifecycleManager.OnStart();
            _timer.RestartTimer();
        }

        private void FinishGame()
        {
            _enemyWaveObserver.OnAllEnemiesDead -= FinishGame;
            if (_playerBrain.IsDead) return;

            _timer.StopTimer();
            _lifecycleManager.OnFinish();
            _charactersDataHandler.SetNextCharacterDialogueGroup(DialogueCharacterID.MainScientist);
            _saveLoadManager.SaveGame();
        }

        //TODO: replace reload scene by reInitialization all systems to better performance

        public void RetryGameLoop()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoNextLevel()
        {
            SceneManager.LoadScene("ScienceBase"); //(_nextSceneName);
        }

        [Button]
        private void ResetGame()
        {
            _saveLoadManager.ResetGame();
            RetryGameLoop();
        }
    }
}