using System;
using Cysharp.Threading.Tasks;
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
            DeathDelayAsync().Forget();
        }

        private async UniTaskVoid DeathDelayAsync()
        {
            await UniTask.WaitForSeconds(0.3f);
            OnDeathAnimationEnd?.Invoke();
        }

        public void EndWaitingTransition()
        {
            OnWaitingTransitionEnd?.Invoke();
        }
    }
}