using System;

namespace YooE.Diploma
{
    public sealed class EnemyWaveObserver
    {
        public event Action OnAllEnemiesDead;
        private readonly EnemyBrainsInitializer _enemyBrainsInitializer;

        public EnemyWaveObserver(EnemyBrainsInitializer enemyBrainsInitializer)
        {
            _enemyBrainsInitializer = enemyBrainsInitializer;
            _enemyBrainsInitializer.OnLiveEnemiesCountChanged += CheckEnemiesRemains;
        }

        private void CheckEnemiesRemains(int diedCount, int allCount)
        {
            if (diedCount != allCount) return;

            OnAllEnemiesDead?.Invoke();
        }
    }
}