using System;
using UnityEngine;

namespace YooE
{
    public sealed class ConfirmPopupView : PopupView
    {
        public event Action OnConfirm;
        public event Action OnDecline;

        [SerializeField] private ButtonView _confirmButton;
        [SerializeField] private ButtonView _declineButton;

        private void OnEnable()
        {
            _confirmButton.OnButtonClicked += Confirm;
            _declineButton.OnButtonClicked += Decline;
        }

        private void OnDisable()
        {
            _confirmButton.OnButtonClicked -= Confirm;
            _declineButton.OnButtonClicked -= Decline;
        }

        private void Confirm()
        {
            OnConfirm?.Invoke();
        }

        private void Decline()
        {
            OnDecline?.Invoke();
        }
    }

    public class PopupView : MonoBehaviour
    {
        public event Action OnClose;

        [SerializeField] protected ButtonView _closeButton;
        [SerializeField] protected GameObject _popupGameObject;

        private void OnEnable()
        {
            _closeButton.OnButtonClicked += Hide;
        }

        private void OnDisable()
        {
            _closeButton.OnButtonClicked -= Hide;
        }

        private void Close()
        {
            OnClose?.Invoke();
        }

        public void Hide()
        {
           // _popupGameObject.transform.DOScale(0f, 0.5f).SetLink(_popupGameObject).Play();
            _popupGameObject.SetActive(false);
          //  _popupGameObject.transform.DOScale(1, 0f).SetLink(_popupGameObject).Play();
        } 
        
        public void HideNoAnimation()
        {
            _popupGameObject.SetActive(false);
        }

        public void Show()
        {
            //_popupGameObject.transform.DOScale(0f, 0f).SetLink(_popupGameObject).Play();
            _popupGameObject.SetActive(true);
            //_popupGameObject.transform.DOScale(1, 0.5f).SetLink(_popupGameObject).Play();
        }
    }
}