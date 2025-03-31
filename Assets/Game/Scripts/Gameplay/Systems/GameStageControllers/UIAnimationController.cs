using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class UIAnimationController : MonoBehaviour, Listeners.IInitListener
    {
        public event Action OnWaitingEnd;
        public event Action OnItemPicked;
        public event Action OnGetMeasurement;

        private static readonly int WaitingTransition = Animator.StringToHash("WaitingTransition");
        [SerializeField] private Animator _fadeAnimator;
        private AnimationEvents _fadeEvents;

        [SerializeField] private GameObject _measureStickPickPopup;
        [SerializeField] private GameObject _notepadPickPopup;
        [SerializeField] private float _timeToPickupAnimation;

        [SerializeField] private GameObject _notepadWritingPopup;

        public void OnInit()
        {
            _fadeAnimator.gameObject.SetActive(true);
            _fadeAnimator.ResetTrigger(WaitingTransition);
            _fadeEvents = _fadeAnimator.gameObject.GetComponent<AnimationEvents>();
            _measureStickPickPopup.SetActive(false);
            _notepadPickPopup.SetActive(false);
            _notepadWritingPopup.SetActive(false);
        }

        public async UniTaskVoid StartWritingAnimation()
        {
            _notepadWritingPopup.SetActive(true);
            await AsyncCountdown(_timeToPickupAnimation);
            _notepadWritingPopup.SetActive(false);
            OnGetMeasurement?.Invoke();
        }

        public void ShowWaitingFade()
        {
            _fadeAnimator.SetTrigger(WaitingTransition);
            _fadeEvents.OnWaitingTransitionEnd += EndWaitingTransition;
        }

        private void EndWaitingTransition()
        {
            _fadeEvents.OnWaitingTransitionEnd -= EndWaitingTransition;
            OnWaitingEnd?.Invoke();
        }

        public async UniTaskVoid StartNotebookPickAnimation()
        {
            _notepadPickPopup.gameObject.SetActive(true);
            await AsyncCountdown(_timeToPickupAnimation);
            _notepadPickPopup.gameObject.SetActive(false);
            OnItemPicked?.Invoke();
        }

        public async UniTaskVoid StartMeasureStickPickAnimation()
        {
            _measureStickPickPopup.gameObject.SetActive(true);
            await AsyncCountdown(_timeToPickupAnimation);
            _measureStickPickPopup.gameObject.SetActive(false);
            OnItemPicked?.Invoke();
        }

        private async UniTask AsyncCountdown(float time)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: CancellationToken.None);
        }
    }
}