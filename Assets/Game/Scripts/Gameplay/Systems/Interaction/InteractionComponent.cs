using UnityEngine;

namespace YooE.Diploma.Interaction
{
    public abstract class InteractionComponent : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _interactionButton;

        public void OnInit()
        {
            DisableInteractView();
        }

        public virtual void Interact()
        {
            Debug.Log("Interact with " + gameObject.name);
        }

        public virtual void EnableInteractView()
        {
            _interactionButton.SetActive(true);
        }

        public virtual void DisableInteractView()
        {
            _interactionButton.SetActive(false);
        }
    }
}