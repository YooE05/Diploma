using System;
using System.Collections.Generic;

namespace YooE.Diploma
{
    public sealed class EnemyBrainsInitializer : Listeners.IStartListener, Listeners.IUpdateListener
    {
        public event Action<int, int> OnLiveEnemiesCountChanged;
        public event Action<EnemyBrain> OnBrainDied;

        // private readonly EnemyConfig _enemyConfig;
        private readonly List<EnemyBrain> _enemyBrains = new();
        //private List<EnemyView> _enemyViews;

        private int EnemiesCount => _enemyBrains.Count;
        private int _deadEnemiesCount;

        /*public EnemyBrainsInitializer(EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
        }*/

        public void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < EnemiesCount; i++)
            {
                _enemyBrains[i].Update();
            }
        }

        public void InitBrain(EnemyView view, EnemyConfig config)
        {
            // _enemyViews = views;

            var newEnemyBrain = new EnemyBrain(config, view);
            newEnemyBrain.OnDied += IncreaseDiedCount;
            _enemyBrains.Add(newEnemyBrain);
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

        private void IncreaseDiedCount(EnemyBrain deadBrain)
        {
            _deadEnemiesCount += 1;
            OnBrainDied?.Invoke(deadBrain);
            deadBrain.OnDied -= IncreaseDiedCount;
            OnLiveEnemiesCountChanged?.Invoke(_deadEnemiesCount, EnemiesCount);
        }
    }
}