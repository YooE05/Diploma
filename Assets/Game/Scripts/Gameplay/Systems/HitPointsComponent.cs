using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private HealthBar _hpSlider;
        public event Action<GameObject> OnHpEmpty;

        public float HitPoints;

        public bool IsHitPointsExists => HitPoints > 0;

        private float _startHp;

        private void Awake()
        {
            if (_startHp == 0)
            {
                _startHp = HitPoints;
            }

            _hpSlider?.SetNewValue(1f);
        }

        public void TakeDamage(float damage)
        {
            HitPoints -= damage;
            if (HitPoints <= 0)
            {
                OnHpEmpty?.Invoke(gameObject);
            }

            _hpSlider?.SetNewValue(HitPoints / _startHp);
        }

        public void SetStartHp(float configHitPoints)
        {
            _startHp = configHitPoints;
            HitPoints = configHitPoints;

            _hpSlider?.SetNewValue(1f);
            _hpSlider?.ResetHpBar();
        }

        public void SetFullHp()
        {
            HitPoints = _startHp;
            _hpSlider?.SetNewValue(1f);
            _hpSlider?.ResetHpBar();
        }
    }
}