using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ShooterGameLoopController : MonoBehaviour, Listeners.IInitListener
    {
        private LifecycleManager _lifecycleManager;
        private EnemyWaveObserver _enemyWaveObserver;
        private PlayerShooterBrain _playerBrain;
        private UpdateTimer _timer;
        private SaveLoadManager _saveLoadManager;

        [SerializeField] private string _nextSceneName;

        [Inject]
        public void Construct(LifecycleManager lifecycleManager, EnemyWaveObserver enemyWaveObserver,
            PlayerShooterBrain playerBrain, UpdateTimer timer, SaveLoadManager saveLoadManager)
        {
            _lifecycleManager = lifecycleManager;
            _enemyWaveObserver = enemyWaveObserver;
            _playerBrain = playerBrain;
            _timer = timer;
            _saveLoadManager = saveLoadManager;

            _enemyWaveObserver.OnAllEnemiesDead += FinishGame;
        }

        public void OnInit()
        {
            // _saveLoadManager.LoadGame();
        }

        private void Start()
        {
            _saveLoadManager.LoadGame();
            // _audioManager.PlaySound("ShooterTheme");
            _lifecycleManager.OnStart();
            _timer.RestartTimer();
        }

        private void FinishGame()
        {
            if (_playerBrain.IsDead) return;

            _timer.StopTimer();
            _saveLoadManager.SaveGame();
            _lifecycleManager.OnFinish();
            //SetNextDialogueGroup();
        }

        //TODO: replace reload scene by reInitialization all systems to better performance

        public void RetryGameLoop()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoNextLevel()
        {
            SceneManager.LoadScene(_nextSceneName);
        }

        [Button]
        private void ResetGame()
        {
            _saveLoadManager.ResetGame();
            RetryGameLoop();
        }
    }
}