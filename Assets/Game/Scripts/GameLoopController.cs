using UnityEngine;
using UnityEngine.SceneManagement;
using YooE;
using YooE.Diploma;
using Zenject;

public sealed class GameLoopController : MonoBehaviour
{
    private LifecycleManager _lifecycleManager;
    private EnemyWaveObserver _enemyWaveObserver;

    private ShooterGameplayScreenPresenter _gameplayScreenPresenter;
    private ShooterPopupsPresenter _popupsPresenter;

    private PlayerDeathObserver _playerDeathObserver;
    private PlayerShooterBrain _playerBrain;

    private UpdateTimer _timer;

    [Inject]
    public void Construct(LifecycleManager lifecycleManager, EnemyWaveObserver enemyWaveObserver,
        ShooterGameplayScreenPresenter gameplayScreenPresenter, ShooterPopupsPresenter popupsPresenter,
        PlayerDeathObserver playerDeathObserver,
        PlayerShooterBrain playerBrain, UpdateTimer timer)
    {
        _lifecycleManager = lifecycleManager;
        _gameplayScreenPresenter = gameplayScreenPresenter;
        _popupsPresenter = popupsPresenter;
        _enemyWaveObserver = enemyWaveObserver;
        _playerDeathObserver = playerDeathObserver;
        _playerBrain = playerBrain;
        _timer = timer;

        _enemyWaveObserver.OnAllEnemiesDead += AllEnemiesDeadActions;
        _playerDeathObserver.OnDeathEnd += PlayerDeathEndActions;
    }

    private void Start()
    {
        _lifecycleManager.OnStart();
        _timer.RestartTimer();
    }

    private void PlayerDeathEndActions()
    {
        _popupsPresenter.ShowRetryPanel();
    }

    private void AllEnemiesDeadActions()
    {
        if (_playerBrain.IsDead) return;

        _timer.StopTimer();

        _gameplayScreenPresenter.HideScreenView();
        _playerBrain.WinActions();
        _popupsPresenter.ShowEndGamePopup();

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
        _enemyWaveObserver.OnAllEnemiesDead -= AllEnemiesDeadActions;
        _playerDeathObserver.OnDeathEnd -= PlayerDeathEndActions;
    }
}