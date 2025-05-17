using System;
using System.Collections.Generic;
using Zenject;

namespace YooE.Diploma
{
    public sealed class EnemiesInitializer : Listeners.IStartListener, Listeners.IUpdateListener,
        Listeners.IFinishListener
    {
        public event Action<int, int, int> OnLiveEnemiesCountChanged;
        public event Action<Enemy> OnEnemyDied;

        private readonly List<Enemy> _enemies = new();

        private int EnemiesCount => _enemies.Count;
        private int _deadEnemiesCount;
        private int _totalDeadEnemiesCount;
        public int BestScore;

        [Inject] private PlayerScoreAndMoneyContainer _playerScoreAndMoneyContainer;

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < EnemiesCount; i++)
            {
                _enemies[i].Update();
            }
        }

        public void OnFinish()
        {
            if (_totalDeadEnemiesCount > BestScore)
            {
                BestScore = _totalDeadEnemiesCount;
            }

            _playerScoreAndMoneyContainer.SetLevelResults(_totalDeadEnemiesCount, BestScore);

            for (var i = 0; i < EnemiesCount; i++)
            {
                _enemies[i].AfterFinishState();
            }
        }

        public void ClearEnemies()
        {
            _enemies.Clear();
        }

        public void InitEnemy(EnemyView view, EnemyConfig config)
        {
            var newEnemy = new Enemy(config, view);
            newEnemy.OnDied += IncreaseDiedCount;
            _enemies.Add(newEnemy);
        }

        public void OnStart()
        {
            BestScore = _playerScoreAndMoneyContainer.BestScore;
            _deadEnemiesCount = 0;
            OnLiveEnemiesCountChanged?.Invoke(_deadEnemiesCount, EnemiesCount, _totalDeadEnemiesCount);
        }

        public float GetDefeatPercent()
        {
            return _deadEnemiesCount / (float)EnemiesCount * 100;
        }

        public int GetTotalDefeatCount()
        {
            return _totalDeadEnemiesCount;
        }

        private void IncreaseDiedCount(Enemy dead)
        {
            _totalDeadEnemiesCount++;
            _deadEnemiesCount += 1;
            OnEnemyDied?.Invoke(dead);
            dead.OnDied -= IncreaseDiedCount;
            OnLiveEnemiesCountChanged?.Invoke(_deadEnemiesCount, EnemiesCount, _totalDeadEnemiesCount);
        }
    }
}