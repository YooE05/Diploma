using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class WaveDataFactory : MonoBehaviour
    {
        [SerializeField] private int _characteristicMaxLevel = 15;
        [SerializeField] private int _countingMaxLevel = 50;

        [SerializeField] private EnemyConfig _startCactusConfig;
        [SerializeField] private EnemyConfig _startMushroomConfig;

        [SerializeField] private EnemyHealthTable _cactusHealthTable;
        [SerializeField] private EnemyHealthTable _mushroomHealthTable;

        [SerializeField] private EnemySpeedTable _cactusSpeedTableTable;
        [SerializeField] private EnemySpeedTable _mushroomSpeedTable;

        [SerializeField] private EnemyCountTable _enemyCountTable;
        [SerializeField] private EnemyDamageTable _enemyDamageTable;

        public void OnValidate()
        {
            _enemyCountTable.UpdateTable(_countingMaxLevel);
            _enemyDamageTable.UpdateTable(_characteristicMaxLevel);

            _cactusHealthTable.UpdateTable(_characteristicMaxLevel);
            _mushroomHealthTable.UpdateTable(_characteristicMaxLevel);

            _cactusSpeedTableTable.UpdateTable(_characteristicMaxLevel);
            _mushroomSpeedTable.UpdateTable(_characteristicMaxLevel);
        }

        public int GetWaveEnemyCount(int waveIndex)
        {
            return _enemyCountTable.GetValue(waveIndex);
        }

        public EnemyWaveData GetWaveData(EnemyType enemyType, int waveIndex)
        {
            var data = enemyType switch
            {
                EnemyType.Cactus => new EnemyWaveData(
                    newSpeed: _cactusSpeedTableTable.GetValue(waveIndex),
                    newDamage: _enemyDamageTable.GetValue(waveIndex),
                    newHp: _cactusHealthTable.GetValue(waveIndex)
                ),
                EnemyType.Mushroom => new EnemyWaveData(
                    newSpeed: _mushroomSpeedTable.GetValue(waveIndex),
                    newDamage: _enemyDamageTable.GetValue(waveIndex),
                    newHp: _mushroomHealthTable.GetValue(waveIndex)
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
            };

            return data;
        }
    }
}