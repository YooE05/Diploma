using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class AnimationEvents : MonoBehaviour
    {
        public event Action OnHitEnded;
        public event Action OnDeathAnimationEnd;
        public event Action OnWaitingTransitionEnd;

        public void DoDamage()
        {
            OnHitEnded?.Invoke();
        }

        public void EndDeathAnimation()
        {
            OnDeathAnimationEnd?.Invoke();
        }

        public void EndWaitingTransition()
        {
            OnWaitingTransitionEnd?.Invoke();
        }
    }
}