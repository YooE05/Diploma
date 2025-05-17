using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public class ShooterGameLoopController : MonoBehaviour
    {
        protected LifecycleManager _lifecycleManager;
        protected EnemyWaveObserver _enemyWaveObserver;
        protected PlayerShooterBrain _playerBrain;
        protected UpdateTimer _timer;
        protected SaveLoadManager _saveLoadManager;
        protected AudioManager _audioManager;
        protected CharactersDataHandler _charactersDataHandler;
        [SerializeField] protected ShooterEndZone _shooterEndZone;

        [SerializeField] protected AudioClip _audioClip;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoNextLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("ScienceBaseVisual"); //(_nextSceneName);
        }

        [Button]
        private void ResetGame()
        {
            _saveLoadManager.ResetGame();
            RetryGameLoop();
        }
    }
}