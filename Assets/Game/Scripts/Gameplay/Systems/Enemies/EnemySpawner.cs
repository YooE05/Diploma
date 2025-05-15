using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemySpawner : Listeners.IInitListener
    {
        private readonly Dictionary<EnemyType, EnemyPoolConfig> _enemyPoolsСonfigs = new();
        private readonly Dictionary<EnemyType, EnemyPool> _enemyPools = new();
        private readonly List<EnemySpawnPoint> _spawnPoints;
        private readonly Transform _enemiesParent;
        private readonly EnemiesInitializer _enemiesInitializer;

        public EnemySpawner(List<EnemySpawnPoint> spawnPoints, EnemyPoolsSettings enemyPoolsSettings,
            Transform enemiesParent, EnemiesInitializer enemiesInitializer)
        {
            for (var i = 0; i < enemyPoolsSettings.EnemyPoolsСonfigs.Count; i++)
            {
                _enemyPoolsСonfigs.Add(enemyPoolsSettings.EnemyPoolsСonfigs[i].Type,
                    enemyPoolsSettings.EnemyPoolsСonfigs[i].PoolConfig);
            }

            _enemiesParent = enemiesParent;
            _spawnPoints = spawnPoints;
            _enemiesInitializer = enemiesInitializer;
        }

        public void OnInit()
        {
            SpawnEnemies();
            _enemiesInitializer.OnEnemyDied += ReturnEnemy;
        }

        private void SpawnEnemies()
        {
            for (var i = 0; i < _spawnPoints.Count; i++)
            {
                var type = _spawnPoints[i].EnemyType;
                if (!_enemyPools.ContainsKey(type))
                {
                    var newPool = new EnemyPool(_enemyPoolsСonfigs[type], _enemiesParent);
                    newPool.OnInit();
                    _enemyPools.Add(type, newPool);
                }

                var newEnemyView = _enemyPools[type].GetEnemy();
                newEnemyView.SetPosition(_spawnPoints[i].SpawnPosition);
                _enemiesInitializer.InitEnemy(newEnemyView, _enemyPoolsСonfigs[type].EnemyCharacteristics);
            }
        }

        private void ReturnEnemy(Enemy enemy)
        {
            var view = enemy.View;
            _enemyPools[view.Type].ReturnEnemy(view);
        }

        ~EnemySpawner()
        {
            _enemiesInitializer.OnEnemyDied -= ReturnEnemy;
        }
    }
}