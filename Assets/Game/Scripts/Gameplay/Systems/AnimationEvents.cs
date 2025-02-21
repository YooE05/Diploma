using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class AnimationEvents : MonoBehaviour
    {
        public event Action OnHitEnded;
        public event Action OnDeathAnimationEnd;

        public void DoDamage()
        {
            OnHitEnded?.Invoke();
        }

        public void EndDeathAnimation()
        {
            OnDeathAnimationEnd?.Invoke();
        }
    }
}