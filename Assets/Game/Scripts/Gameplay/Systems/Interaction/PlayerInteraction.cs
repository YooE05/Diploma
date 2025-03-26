using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma.Interaction
{
    public class PlayerInteraction : MonoBehaviour
    {
        private readonly List<InteractionComponent> _interactionObjectsList = new();

        public void EnableInteraction()
        {
            for (int i = 0; i < _interactionObjectsList.Count; i++)
            {
                _interactionObjectsList[i].EnableInteractView();
            }
        }

        public void DisableInteraction()
        {
            for (int i = 0; i < _interactionObjectsList.Count; i++)
            {
                _interactionObjectsList[i].DisableInteractView();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var enterInteractable = other.GetComponent<InteractionComponent>();
            if (enterInteractable != null)
            {
                _interactionObjectsList.Add(other.GetComponent<InteractionComponent>());
                enterInteractable.EnableInteractView();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var exitInteractable = other.GetComponent<InteractionComponent>();
            if (_interactionObjectsList.Contains(exitInteractable))
            {
                _interactionObjectsList.Remove(other.GetComponent<InteractionComponent>());
                exitInteractable.DisableInteractView();
            }
        }
    }
}