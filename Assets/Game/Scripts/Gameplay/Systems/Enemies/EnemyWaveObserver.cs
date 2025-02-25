using System;

namespace YooE.Diploma
{
    public sealed class EnemyWaveObserver
    {
        public event Action OnAllEnemiesDead;
        private readonly EnemyContainer _enemyContainer;

        public EnemyWaveObserver(EnemyContainer enemyContainer)
        {
            _enemyContainer = enemyContainer;
            _enemyContainer.OnLiveEnemiesCountChanged += CheckEnemiesRemains;
        }

        private void CheckEnemiesRemains(int diedCount, int allCount)
        {
            if (diedCount != allCount) return;

            OnAllEnemiesDead?.Invoke();
        }
    }
}