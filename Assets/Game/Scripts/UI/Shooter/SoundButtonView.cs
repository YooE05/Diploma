using System;
using UnityEngine;

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

        public void SetSoundButtonEnabling(bool isSwitchedOn)
        {
            _soundsButton.SetSwitchPosition(isSwitchedOn);
        }

        private void SoundButtonClicked()
        {
            _soundsButton.Switch();
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