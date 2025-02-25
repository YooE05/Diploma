using System;
using System.Collections.Generic;

namespace YooE.Diploma
{
    public sealed class EnemyContainer : Listeners.IInitListener, Listeners.IUpdateListener,
        Listeners.IStartListener
    {
        public event Action<int, int> OnLiveEnemiesCountChanged;

        private readonly List<EnemyView> _enemyViews = new();
        private readonly List<EnemyBrain> _enemyBrains = new();

        private int EnemiesCount => _enemyBrains.Count;
        private int _deadEnemiesCount;

        private readonly EnemyConfig _enemyConfig;

        //TODO: change get enemyViews to enemyPool spawning
        public EnemyContainer(EnemyConfig enemyConfig, List<EnemyView> enemyViews)
        {
            _enemyConfig = enemyConfig;
            _enemyViews = enemyViews;
        }

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < _enemyViews.Count; i++)
            {
                _enemyBrains[i].Update();
            }
        }

        public void OnInit()
        {
            for (var i = 0; i < _enemyViews.Count; i++)
            {
                var newEnemyBrain = new EnemyBrain(_enemyConfig, _enemyViews[i]);
                newEnemyBrain.OnDead += IncreaseDeadCount;
                _enemyBrains.Add(newEnemyBrain);
            }
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

        private void IncreaseDeadCount(EnemyBrain deadBrain)
        {
            _deadEnemiesCount += 1;
            deadBrain.OnDead -= IncreaseDeadCount;
            OnLiveEnemiesCountChanged?.Invoke(_deadEnemiesCount, EnemiesCount);
        }
    }
}