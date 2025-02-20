using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemyAnimationEvents : MonoBehaviour
    {
        public event Action OnPunchEnded;
        public event Action OnDeathAnimationEnd;

        public void DoDamage()
        {
            OnPunchEnded?.Invoke();
        }

        public void EndDeathAnimation()
        {
            OnDeathAnimationEnd?.Invoke();
        }
    }
}