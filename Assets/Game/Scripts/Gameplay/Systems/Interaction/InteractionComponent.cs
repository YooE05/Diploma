using UnityEngine;
using UnityEngine.UI;

namespace YooE.Diploma.Interaction
{
    public abstract class InteractionComponent : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _interactionButtonView;
        [SerializeField] private Button _interactionButton;

        public virtual void OnInit()
        {
            DisableInteractView();
        }

        public virtual void Interact()
        {
            Debug.Log("Interact with " + gameObject.name);
        }

        public virtual void EnableInteractView()
        {
            _interactionButtonView.SetActive(true);
            _interactionButton.onClick.AddListener(Interact);
        }

        public virtual void DisableInteractView()
        {
            _interactionButtonView.SetActive(false);
            _interactionButton.onClick.RemoveListener(Interact);
        }
    }
}