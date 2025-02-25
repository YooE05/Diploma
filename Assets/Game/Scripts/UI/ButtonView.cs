using System;
using UnityEngine;
using UnityEngine.UI;

namespace YooE
{
    public class ButtonView : MonoBehaviour
    {
        public event Action OnButtonClicked;

        [SerializeField] protected Button _button;

        [SerializeField] protected Sprite _enableSprite;
        [SerializeField] protected Sprite _disableSprite;

        private void OnEnable()
        {
            _button.onClick.AddListener(InvokeButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            _button.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _button.gameObject.SetActive(false);
        }

        public void EnableButton()
        {
            _button.enabled = true;
            _button.image.sprite = _enableSprite;
        }

        public void DisableButton()
        {
            _button.enabled = false;
            _button.image.sprite = _disableSprite;
        }

        private void InvokeButtonClick()
        {
            OnButtonClicked?.Invoke();
        }
    }
}