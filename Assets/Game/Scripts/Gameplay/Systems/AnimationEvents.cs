using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class AnimationEvents : MonoBehaviour
    {
        public event Action OnHitEnded;
        public event Action OnDeathAnimationEnd;
        public event Action OnWaitingTransitionEnd;

        private CancellationTokenSource _cancellationTokenSource;

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
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            await UniTask.WaitForSeconds(0.3f, true, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            OnDeathAnimationEnd?.Invoke();
        }

        public void EndWaitingTransition()
        {
            OnWaitingTransitionEnd?.Invoke();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}