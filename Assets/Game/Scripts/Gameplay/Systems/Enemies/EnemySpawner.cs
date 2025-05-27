using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyWave
    {
        public EnemyWave(int enemyCount, Dictionary<EnemyType, EnemyWaveData> enemyWaveData)
        {
            EnemyCount = enemyCount;
            EnemyWaveData = enemyWaveData;
        }

        public int EnemyCount { get; }
        public Dictionary<EnemyType, EnemyWaveData> EnemyWaveData { get; }
    }

    public sealed class EnemySpawner : Listeners.IInitListener
    {
        private readonly Dictionary<EnemyType, EnemyPoolConfig> _enemyPoolsConfigs = new();
        private readonly Dictionary<EnemyType, EnemyPool> _enemyPools = new();
        private readonly List<EnemySpawnPoint> _spawnPoints;
        private readonly Transform _enemiesParent;
        private readonly EnemiesInitializer _enemiesInitializer;

        private readonly bool _needWaveSpawn;

        public EnemySpawner(List<EnemySpawnPoint> spawnPoints, EnemyPoolsSettings enemyPoolsSettings,
            Transform enemiesParent, EnemiesInitializer enemiesInitializer, bool needWaveSpawn = false)
        {
            for (var i = 0; i < enemyPoolsSettings.EnemyPoolsСonfigs.Count; i++)
            {
                _enemyPoolsConfigs.Add(enemyPoolsSettings.EnemyPoolsСonfigs[i].Type,
                    enemyPoolsSettings.EnemyPoolsСonfigs[i].PoolConfig);
            }

            _enemiesParent = enemiesParent;
            _spawnPoints = spawnPoints;
            _enemiesInitializer = enemiesInitializer;

            _needWaveSpawn = needWaveSpawn;
        }

        public void OnInit()
        {
            if (!_needWaveSpawn)
            {
                SpawnEnemies();
            }

            _enemiesInitializer.OnEnemyDied += ReturnEnemy;
        }

        public void SpawnEnemyWave(EnemyWave enemyWave)
        {
            _enemiesInitializer.ClearEnemies();

            if (_spawnPoints.Count == 0)
            {
                Debug.LogWarning("No Enemy Spawnpoints");
                return;
            }

            var currentSpawnPoints = _spawnPoints.GetRandomItemsFisherYates(enemyWave.EnemyCount);
            while (currentSpawnPoints.Count < enemyWave.EnemyCount)
            {
                currentSpawnPoints.AddRange(
                    _spawnPoints.GetRandomItemsFisherYates(enemyWave.EnemyCount - currentSpawnPoints.Count));
            }

            for (var i = 0; i < currentSpawnPoints.Count; i++)
            {
                var type = currentSpawnPoints[i].EnemyType;
                if (!_enemyPools.ContainsKey(type))
                {
                    var newPool = new EnemyPool(_enemyPoolsConfigs[type], _enemiesParent);
                    newPool.OnInit();
                    _enemyPools.Add(type, newPool);
                }

                var newEnemyView = _enemyPools[type].GetEnemy();
                newEnemyView.SetPosition(currentSpawnPoints[i].SpawnPosition);
                newEnemyView.SetRandomRotation();
                _enemiesInitializer.InitEnemy(newEnemyView, 
                    _enemyPoolsConfigs[type].EnemyCharacteristics.GetCloneWithNewValues(enemyWave.EnemyWaveData[type]));
            }

            _enemiesInitializer.OnStart();
        }

        private void SpawnEnemies()
        {
            for (var i = 0; i < _spawnPoints.Count; i++)
            {
                if (_spawnPoints[i].EnemyType == EnemyType.Any) continue;

                var type = _spawnPoints[i].EnemyType;
                if (!_enemyPools.ContainsKey(type))
                {
                    var newPool = new EnemyPool(_enemyPoolsConfigs[type], _enemiesParent);
                    newPool.OnInit();
                    _enemyPools.Add(type, newPool);
                }

                var newEnemyView = _enemyPools[type].GetEnemy();
                newEnemyView.SetPosition(_spawnPoints[i].SpawnPosition);
                newEnemyView.SetRandomRotation();
                _enemiesInitializer.InitEnemy(newEnemyView, _enemyPoolsConfigs[type].EnemyCharacteristics);
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