using System;
using DG.Tweening;
using UnityEngine;

namespace YooE.Diploma.Interaction
{
    public abstract class InteractionComponent : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _interactionButtonView;
        [SerializeField] private ButtonView _interactionButton;

        private bool _isInteractable;
        private Vector3 _initScale;

        public virtual void OnInit()
        {
            DisableInteractView();
            EnableInteractionAbility();
        }

        private void Awake()
        {
            _initScale = _interactionButtonView.transform.localScale;
            _interactionButtonView.transform.DOScale(_initScale * 1.2f, 0.7f).From(_initScale)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(_interactionButtonView).Play();
        }

        public virtual void Interact()
        {
            Debug.Log("Interact with " + gameObject.name);
        }

        public virtual void EnableInteractView()
        {
            if (!_isInteractable) return;

            _interactionButtonView.SetActive(true);
            _interactionButton.OnButtonClicked += Interact;
        }

        public virtual void DisableInteractView()
        {
            _interactionButtonView.SetActive(false);
            _interactionButton.OnButtonClicked -= Interact;
        }

        public void DisableInteractionAbility()
        {
            _isInteractable = false;
        }

        public void EnableInteractionAbility()
        {
            _isInteractable = true;
        }
    }
}