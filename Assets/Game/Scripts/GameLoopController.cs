using UnityEngine;
using UnityEngine.SceneManagement;
using YooE;
using YooE.Diploma;
using Zenject;

public sealed class GameLoopController : MonoBehaviour
{
    private LifecycleManager _lifecycleManager;
    private EnemyWaveObserver _enemyWaveObserver;
    private PlayerShooterBrain _playerBrain;
    private UpdateTimer _timer;

    [Inject]
    public void Construct(LifecycleManager lifecycleManager, EnemyWaveObserver enemyWaveObserver,
        PlayerShooterBrain playerBrain, UpdateTimer timer)
    {
        _lifecycleManager = lifecycleManager;
        _enemyWaveObserver = enemyWaveObserver;
        _playerBrain = playerBrain;
        _timer = timer;

        _enemyWaveObserver.OnAllEnemiesDead += FinishGame;
    }

    private void Start()
    {
        _lifecycleManager.OnStart();
        _timer.RestartTimer();
    }

    private void FinishGame()
    {
        if (_playerBrain.IsDead) return;

        _timer.StopTimer();
        _lifecycleManager.OnFinish();
    }

    public void RetryGameLoop()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //TODO: add sceneLoaderManager

    public void GoNextLevel()
    {
        Debug.Log("IT'S THE NEXT LEVEL");
    }

    //TODO: replace reload scene by reInitialization all systems to better performance
    ~GameLoopController()
    {
        _enemyWaveObserver.OnAllEnemiesDead -= FinishGame;
    }
}