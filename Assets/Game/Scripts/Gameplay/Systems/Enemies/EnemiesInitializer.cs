using System;
using System.Collections.Generic;

namespace YooE.Diploma
{
    public sealed class EnemiesInitializer : Listeners.IStartListener, Listeners.IUpdateListener,
        Listeners.IFinishListener
    {
        public event Action<int, int> OnLiveEnemiesCountChanged;
        public event Action<Enemy> OnEnemyDied;

        private readonly List<Enemy> _enemies = new();

        private int EnemiesCount => _enemies.Count;
        private int _deadEnemiesCount;

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < EnemiesCount; i++)
            {
                _enemies[i].Update();
            }
        }

        public void OnFinish()
        {
            for (var i = 0; i < EnemiesCount; i++)
            {
                _enemies[i].AfterFinishState();
            }
        }

        public void InitEnemy(EnemyView view, EnemyConfig config)
        {
            var newEnemy = new Enemy(config, view);
            newEnemy.OnDied += IncreaseDiedCount;
            _enemies.Add(newEnemy);
        }

        public void OnStart()
        {
            _deadEnemiesCount = 0;
            OnLiveEnemiesCountChanged?.Invoke(_deadEnemiesCount, EnemiesCount);
        }

        public float GetDefeatPercent()
        {
            return _deadEnemiesCount / (float)EnemiesCount * 100;
        }

        private void IncreaseDiedCount(Enemy dead)
        {
            _deadEnemiesCount += 1;
            OnEnemyDied?.Invoke(dead);
            dead.OnDied -= IncreaseDiedCount;
            OnLiveEnemiesCountChanged?.Invoke(_deadEnemiesCount, EnemiesCount);
        }
    }
}