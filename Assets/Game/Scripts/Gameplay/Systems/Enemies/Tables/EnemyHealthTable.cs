using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace YooE
{
    [Serializable]
    public class EnemyHealthTable : EvaluationTable<float>
    {
        [SerializeField] private float _hpGrowthRate = 0.05f;

        protected override float EvaluateFunction(int level)
        {
            return _startValue * Mathf.Pow(1f + _hpGrowthRate, level - 1);
        }
    }

    [Serializable]
    public class EnemyDamageTable : EvaluationTable<float>
    {
        [SerializeField] private float _damageGrowthRate = 0.03f;

        protected override float EvaluateFunction(int level)
        {
            var damage = _startValue * Mathf.Pow(1f + _damageGrowthRate, level - 1);
            return damage;
        }
    }

    [Serializable]
    public class EnemySpeedTable : EvaluationTable<float>
    {
        [SerializeField] private float _speedGrowthB = 0.25f;
        [SerializeField] private float _maxSpeed = 4f;

        protected override float EvaluateFunction(int level)
        {
            var speed = _startValue + _speedGrowthB * Mathf.Log(level);
            return Mathf.Min(speed, _maxSpeed);
        }
    }

    [Serializable]
    public class EnemyCountTable : EvaluationTable<int>
    {
        [SerializeField] private float _countGrowthA = 2f;
        [SerializeField] private float _countExponentP = 1.1f;

        protected override int EvaluateFunction(int level)
        {
            var count = _startValue + _countGrowthA * Mathf.Pow(level - 1, _countExponentP);
            return Mathf.CeilToInt(count);
        }
    }

    [Serializable]
    public abstract class EvaluationTable<T>
    {
        [SerializeField] protected T _startValue;

        [Space]
        [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")]
        [SerializeField]
        private T[] _levels;

        public T GetValue(int levelIndex)
        {
            levelIndex = Mathf.Clamp(levelIndex, 0, _levels.Length - 1);
            return _levels[levelIndex];
        }

        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }

        public void UpdateTable(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            var table = new T[maxLevel];
            table[0] = _startValue;
            for (var level = 2; level <= maxLevel; level++)
            {
                var currentLevelValue = EvaluateFunction(level);
                table[level - 1] = (T)currentLevelValue;
            }

            _levels = table;
        }

        protected abstract T EvaluateFunction(int level);
    }
}