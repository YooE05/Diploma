using System.Collections.Generic;
using Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooE.DialogueSystem;
using YooE.SaveLoad;
using Zenject;

namespace YooE.Diploma
{
    public sealed class EndlessShooterGameLoop : MonoBehaviour
    {
        [SerializeField] private ButtonView _startButton;
        [SerializeField] private GameObject _startPanel;

        private LifecycleManager _lifecycleManager;
        private PlayerDeathObserver _playerDeathObserver;
        private EnemyWaveObserver _enemyWaveObserver;
        private PlayerShooterBrain _playerBrain;
        private UpdateTimer _timer;
        private SaveLoadManager _saveLoadManager;
        private AudioManager _audioManager;
        [SerializeField] private AudioClip _audioClip;

        private EnemySpawner _enemySpawner;

        [SerializeField] private ShooterGameplayScreenView _gameplayScreenView;
        [SerializeField] private ShooterQuestionsGenerator _questionsGenerator;
        [SerializeField] private WaveDataFactory _waveDataFactory;
        private int _waveIndex;

        [Inject]
        public void Construct(LifecycleManager lifecycleManager, EnemyWaveObserver enemyWaveObserver,
            EnemySpawner enemySpawner,
            PlayerShooterBrain playerBrain, UpdateTimer timer, SaveLoadManager saveLoadManager,
            AudioManager audioManager, CharactersDataHandler charactersDataHandler,
            PlayerDeathObserver playerDeathObserver)
        {
            _playerDeathObserver = playerDeathObserver;
            _playerDeathObserver.OnDeathEnd += FinishGame;

            _audioManager = audioManager;
            _lifecycleManager = lifecycleManager;
            _enemyWaveObserver = enemyWaveObserver;
            _enemySpawner = enemySpawner;
            _playerBrain = playerBrain;
            _timer = timer;

            _saveLoadManager = saveLoadManager;

            _gameplayScreenView.HideWaveNumber();
        }

        private void Start()
        {
            _saveLoadManager.OnDataLoaded += InitGameplay;
            _saveLoadManager.LoadGame();
            _waveIndex = 0;
        }

        private void InitGameplay()
        {
            _saveLoadManager.OnDataLoaded -= InitGameplay;

            _audioManager.PlaySound(_audioClip, AudioOutput.Music);

            _gameplayScreenView.Hide();
            _startPanel.SetActive(true);
            _startButton.OnButtonClicked += StartGame;
        }

        private void StartGame()
        {
            SpawnEnemyWave(0f).Forget();
            _gameplayScreenView.Show();
            _startPanel.SetActive(false);
            _startButton.OnButtonClicked -= StartGame;
            
            _lifecycleManager.OnStart();
            _timer.RestartTimer();
        }

        private async UniTaskVoid DelayedStart()
        {
            await UniTask.WaitForSeconds(2f);

            _lifecycleManager.OnStart();
            _timer.RestartTimer();
        }

        private async UniTaskVoid SpawnEnemyWave(float delay)
        {
            _gameplayScreenView.ShowWaveNumber(_waveIndex + 1);
            await UniTask.WaitForSeconds(delay);

            _enemyWaveObserver.OnAllEnemiesDead += DefeatWave;

            _enemySpawner.SpawnEnemyWave(new EnemyWave(
                enemyCount: _waveDataFactory.GetWaveEnemyCount(_waveIndex),
                enemyWaveData: new Dictionary<EnemyType, EnemyWaveData>
                {
                    {
                        EnemyType.Cactus, _waveDataFactory.GetWaveData(EnemyType.Cactus, _waveIndex)
                    },
                    {
                        EnemyType.Mushroom, _waveDataFactory.GetWaveData(EnemyType.Mushroom, _waveIndex)
                    },
                }
            ));

            _gameplayScreenView.HideWaveNumber();
        }

        private void DefeatWave()
        {
            _enemyWaveObserver.OnAllEnemiesDead -= DefeatWave;
            if (_playerBrain.IsDead) return;

            _waveIndex++;

            _questionsGenerator.OnBadAnswer += OnBadAnswer;
            _questionsGenerator.OnGoodAnswer += OnGoodAnswer;
            _questionsGenerator.ShowQuestion();
        }

        private void OnGoodAnswer()
        {
            //Good effects
            _playerBrain.GetHitPointsComponent().SetFullHp();
            RestartWave();
        }

        private void OnBadAnswer()
        {
            //Bad effects
            RestartWave();
        }

        private void RestartWave()
        {
            _questionsGenerator.OnBadAnswer -= OnBadAnswer;
            _questionsGenerator.OnGoodAnswer -= OnGoodAnswer;

            SpawnEnemyWave(2f).Forget();
        }

        private void FinishGame()
        {
            _playerDeathObserver.OnDeathEnd -= FinishGame;
            _timer.StopTimer();
            _lifecycleManager.OnFinish();

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
            SceneManager.LoadScene("ScienceBaseVisual");
        }
    }
}