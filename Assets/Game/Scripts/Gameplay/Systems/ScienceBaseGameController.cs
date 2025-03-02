using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace YooE.Diploma
{
    public sealed class ScienceBaseGameController : MonoBehaviour
    {
        private LifecycleManager _lifecycleManager;

        [SerializeField] private string _shooterSceneName;

        [Inject]
        public void Construct(LifecycleManager lifecycleManager, EnemyWaveObserver enemyWaveObserver,
            PlayerShooterBrain playerBrain, UpdateTimer timer)
        {
            _lifecycleManager = lifecycleManager;
        }

        private void Start()
        {
            _lifecycleManager.OnStart();
        }

        private void FinishGame()
        {
            _lifecycleManager.OnFinish();
        }

        public void GoToShooterScene()
        {
            SceneManager.LoadScene(_shooterSceneName);
        }
    }
}