using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private HealthBar _hpSlider;
        public event Action<GameObject> OnHpEmpty;

        public int HitPoints;

        public bool IsHitPointsExists => HitPoints > 0;

        private int _startHp;

        private void Awake()
        {
            _hpSlider?.SetNewValue(1f);
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
            if (HitPoints <= 0)
            {
                OnHpEmpty?.Invoke(gameObject);
            }

            _hpSlider?.SetNewValue(HitPoints / (float)_startHp);
        }

        public void SetStartHp(int enemyConfigHitPoints)
        {
            _startHp = enemyConfigHitPoints;
            HitPoints = enemyConfigHitPoints;
        }
    }
}