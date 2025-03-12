using System;
using Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace YooE.Diploma
{
    public sealed class SoundButtonView : MonoBehaviour
    {
        public event Action<bool> OnSoundButtonClicked;

        [SerializeField] private SwitchButtonView _soundsButton;

        private void OnEnable()
        {
            _soundsButton.OnButtonClicked += SoundButtonClicked;
        }

        private void OnDisable()
        {
            _soundsButton.OnButtonClicked -= SoundButtonClicked;
        }

        private AudioMixerSnapshot _pauseSnapshot;
        private AudioMixerSnapshot _unPauseSnapshot;
        private bool _isPause;

        private void Start()
        {
            Audio.AudioManager.Instance.TryGetSnapshot(AudioManagerStaticData.PAUSE_SNAPSHOT_NAME, out _pauseSnapshot);
            Audio.AudioManager.Instance.TryGetSnapshot(AudioManagerStaticData.UNPAUSE_SNAPSHOT_NAME,
                out _unPauseSnapshot);
        }

        public void SetSoundButtonEnabling(bool isSwitchedOn)
        {
            _soundsButton.SetSwitchPosition(isSwitchedOn);
            Audio.AudioManager.Instance.Transition(!isSwitchedOn ? _pauseSnapshot : _unPauseSnapshot);
        }

        private void SoundButtonClicked()
        {
            _soundsButton.Switch();
            Audio.AudioManager.Instance.Transition(!_soundsButton.IsSwitchedOn ? _pauseSnapshot : _unPauseSnapshot);
            OnSoundButtonClicked?.Invoke(_soundsButton.IsSwitchedOn);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}