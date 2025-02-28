using UnityEngine;

namespace YooE
{
    public class SwitchButtonView : ButtonView
    {
        [SerializeField] private Sprite _switchOnSprite;
        [SerializeField] private Sprite _switchOffSprite;

        public bool IsSwitchedOn { get; private set; }

        public void SetSwitchPosition(bool isOn)
        {
            IsSwitchedOn = isOn;
            SetSwitchedSprite();
        }

        public void Switch()
        {
            IsSwitchedOn = !IsSwitchedOn;
            SetSwitchedSprite();
        }

        private void SetSwitchedSprite()
        {
            if (_button.enabled)
            {
                _button.image.sprite = IsSwitchedOn ? _switchOnSprite : _switchOffSprite;
            }
        }
    }
}