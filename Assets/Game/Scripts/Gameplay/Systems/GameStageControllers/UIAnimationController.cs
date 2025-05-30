using System;
using System.Threading;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace YooE.Diploma
{
    public sealed class UIAnimationController : MonoBehaviour, Listeners.IInitListener
    {
        public event Action OnWaitingEnd;
        public event Action OnItemPicked;
        public event Action OnGetMeasurement;

        [Inject] private AudioManager _audioManager;

        [Header("Find Objects")]
        [SerializeField]
        private GameObject _measureStickPickPopup;

        [SerializeField] private GameObject _measureStickBack;

        [SerializeField] private GameObject _notepadPickPopup;
        [SerializeField] private GameObject _notepadPickBack;
        [SerializeField] private float _timeToPickupAnimation;

        [Header("Notepad Writing")]
        [SerializeField]
        private GameObject _notepadWritingPopup;

        [SerializeField] private TextMeshProUGUI _measurementText1;
        [SerializeField] private TextMeshProUGUI _measurementText2;
        [SerializeField] private TextMeshProUGUI _measurementText3;

        [Header("Waiting fade")]
        [SerializeField]
        private GameObject _fadeBack;

        [SerializeField] private GameObject _fadeClock;

        public void OnInit()
        {
            _fadeBack.SetActive(false);
            _measureStickPickPopup.SetActive(false);
            _notepadPickPopup.SetActive(false);
            _notepadWritingPopup.SetActive(false);
        }

        public async UniTaskVoid StartWritingAnimation()
        {
            _notepadWritingPopup.SetActive(true);

            var sequence = DOTween.Sequence();
            PlaySoundByName("pen");
            sequence.Append(
                _measurementText1.DOFade(1f, _timeToPickupAnimation / 3f).From(0)
                    .OnComplete(() => PlaySoundByName("pen")));
            sequence.Append(_measurementText2.DOFade(1f, _timeToPickupAnimation / 3f).From(0)
                .OnComplete(() => PlaySoundByName("pen")));
            sequence.Append(_measurementText3.DOFade(1f, _timeToPickupAnimation / 3f).From(0));
            sequence.SetLink(_notepadWritingPopup).Play();

            await AsyncCountdown(_timeToPickupAnimation + 1f);
            _notepadWritingPopup.SetActive(false);
            OnGetMeasurement?.Invoke();
        }

        private void PlaySoundByName(string soundName)
        {
            if (_audioManager.TryGetAudioClipByName(soundName, out var audioClip))
            {
                _audioManager.PlaySoundOneShot(audioClip, AudioOutput.UI);
            }
        }

        public async UniTaskVoid ShowWaitingFade()
        {
            _fadeClock.SetActive(false);
            _fadeBack.SetActive(true);
            PlaySoundByName("clock");
            _fadeBack.GetComponent<Image>().DOFade(1f, 0.7f).From(0).SetLink(_fadeBack).Play();

            await AsyncCountdown(0.7f);
            _fadeClock.SetActive(true);
            _fadeClock.GetComponent<Image>().DOFade(1f, 1f).From(0).SetLink(_fadeClock)
                .Play();

            await AsyncCountdown(1f);
            _fadeClock.GetComponent<Image>().DOFade(0f, 1f).From(1).SetLink(_fadeClock)
                .Play();

            await AsyncCountdown(1f);
            _fadeBack.GetComponent<Image>().DOFade(0f, 0.7f).From(1).SetLink(_fadeBack)
                .OnComplete(() =>
                {
                    OnWaitingEnd?.Invoke();
                    _fadeBack.SetActive(false);
                }).Play();
        }

        public async UniTaskVoid StartNotebookPickAnimation()
        {
            _notepadPickPopup.gameObject.SetActive(true);
            _notepadPickPopup.transform.DOScale(1f, 0.5f).From(0).SetLink(_notepadPickPopup).Play();
            _notepadPickBack.transform.DORotate(new Vector3(0f, 0f, 360f), 7f, RotateMode.FastBeyond360)
                .SetLink(_notepadPickPopup).Play();

            await AsyncCountdown(_timeToPickupAnimation);
            _notepadPickPopup.transform.DOScale(0f, 0.3f).SetLink(_notepadPickPopup).Play();
            await AsyncCountdown(0.3f);
            _notepadPickPopup.gameObject.SetActive(false);
            OnItemPicked?.Invoke();
        }

        public async UniTaskVoid StartMeasureStickPickAnimation()
        {
            _measureStickPickPopup.gameObject.SetActive(true);
            _measureStickPickPopup.transform.DOScale(1f, 0.5f).From(0).SetLink(_measureStickPickPopup).Play();
            _measureStickBack.transform.DORotate(new Vector3(0f, 0f, 360f), 7f, RotateMode.FastBeyond360)
                .SetLink(_notepadPickPopup).Play();

            await AsyncCountdown(_timeToPickupAnimation);
            _measureStickPickPopup.transform.DOScale(0f, 0.3f).SetLink(_measureStickPickPopup);
            await AsyncCountdown(0.3f);
            _measureStickPickPopup.gameObject.SetActive(false);
            OnItemPicked?.Invoke();
        }

        private async UniTask AsyncCountdown(float time)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: CancellationToken.None);
        }
    }
}