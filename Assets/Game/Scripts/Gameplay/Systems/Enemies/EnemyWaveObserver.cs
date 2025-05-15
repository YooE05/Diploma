using System;

namespace YooE.Diploma
{
    public sealed class EnemyWaveObserver
    {
        public event Action OnAllEnemiesDead;
        private readonly EnemiesInitializer _enemiesInitializer;

        public EnemyWaveObserver(EnemiesInitializer enemiesInitializer)
        {
            _enemiesInitializer = enemiesInitializer;
            _enemiesInitializer.OnLiveEnemiesCountChanged += CheckEnemiesRemains;
        }

        private void CheckEnemiesRemains(int diedCount, int allCount)
        {
            if (diedCount != allCount) return;

            OnAllEnemiesDead?.Invoke();
        }
    }
}