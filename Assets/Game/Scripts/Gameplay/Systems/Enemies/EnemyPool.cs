using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyPool : Pool<EnemyView>
    {
        public EnemyPool(GameObjectsPoolConfig config, Transform initParent)
        {
            _initParent = initParent;
            _prefab = config.Prefab.GetComponent<EnemyView>();
            _initCount = config.InitCount;

            OnAddObject += AddEnemyActions;
        }

        public EnemyView GetEnemy()
        {
            var enemyView = Get();
            enemyView.EnableEnemy();
            return enemyView;
        }

        public void ReturnEnemy(EnemyView enemy)
        {
            enemy.DisableEnemy();
            Return(enemy);
        }

        private void AddEnemyActions(EnemyView enemy)
        {
            enemy.DisableEnemy();
        }

        public List<EnemyView> GetActiveEnemies()
        {
            return GetActive();
        }
    }
}