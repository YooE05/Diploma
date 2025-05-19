using UnityEngine;

namespace YooE.Diploma
{
    public class StoreItemButton : SwitchButtonViewWithText
    {
        public string Id;
        public bool IsItemUnlocked;
        public bool IsItemEnabled => IsSwitchedOn;

        [SerializeField] private Sprite _lockedSprite;
        [SerializeField] private int _price;
        [SerializeField] private GameObject _sceneObject;

        public void InitItem(bool isUnlocked, bool isEnabled = false)
        {
            IsItemUnlocked = isUnlocked;

            if (!isUnlocked)
            {
                _button.image.sprite = _lockedSprite;
                SetText($"{_price} монет");
                _sceneObject.SetActive(false);
                return;
            }

            SetText(isEnabled ? "включено" : "выключено");
            _sceneObject.SetActive(isEnabled);
            SetSwitchPosition(isEnabled);
        }

        public bool TryBuy(int moneyAmount, out int remainingMoney)
        {
            remainingMoney = moneyAmount;
            if (IsItemUnlocked || moneyAmount < _price) return false;

            IsItemUnlocked = true;
            SetText("включено");
            _sceneObject.SetActive(true);
            SetSwitchPosition(true);

            remainingMoney = moneyAmount - _price;
            return true;
        }

        public void SwitchItemEnabling()
        {
            Switch();
            SetText(IsSwitchedOn ? "включено" : "выключено");
            _sceneObject.SetActive(IsSwitchedOn);
        }
    }
}