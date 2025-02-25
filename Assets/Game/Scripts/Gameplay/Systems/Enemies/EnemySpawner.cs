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
        private readonly EnemyBrainsInitializer _brainsInitializer;

        public EnemySpawner(List<EnemySpawnPoint> spawnPoints, EnemyPoolsSettings enemyPoolsSettings,
            Transform enemiesParent, EnemyBrainsInitializer brainsInitializer)
        {
            for (var i = 0; i < enemyPoolsSettings.EnemyPoolsСonfigs.Count; i++)
            {
                _enemyPoolsСonfigs.Add(enemyPoolsSettings.EnemyPoolsСonfigs[i].Type,
                    enemyPoolsSettings.EnemyPoolsСonfigs[i].PoolConfig);
            }

            // _enemyPoolsСonfigs = enemyPoolsSettings.EnemyPoolsСonfigs;
            _enemiesParent = enemiesParent;
            _spawnPoints = spawnPoints;
            _brainsInitializer = brainsInitializer;
        }

        public void OnInit()
        {
            SpawnEnemies();
            _brainsInitializer.OnBrainDied += ReturnEnemy;
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

                var newEnemy = _enemyPools[type].GetEnemy();
                newEnemy.SetPosition(_spawnPoints[i].SpawnPosition);
                _brainsInitializer.InitBrain(newEnemy, _enemyPoolsСonfigs[type].EnemyCharacteristics);
            }
        }

        /*private List<EnemyView> GetAliveEnemyViews()
        {
            List<EnemyView> allViews = new();
            foreach (var enemies in _enemyPools.Values.Select(pool => pool.GetActiveEnemies()))
            {
                allViews.AddRange(enemies);
            }

            return allViews;
        }*/

        private void ReturnEnemy(EnemyBrain enemyBrain)
        {
            var view = enemyBrain.View;
            _enemyPools[view.Type].ReturnEnemy(view);
        }

        ~EnemySpawner()
        {
            _brainsInitializer.OnBrainDied -= ReturnEnemy;
        }
    }
}