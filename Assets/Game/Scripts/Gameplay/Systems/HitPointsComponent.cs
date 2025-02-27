using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHpEmpty;

        public int HitPoints;

        public bool IsHitPointsExists => HitPoints > 0;

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
            if (HitPoints <= 0)
            {
                OnHpEmpty?.Invoke(gameObject);
            }
        }
    }
}