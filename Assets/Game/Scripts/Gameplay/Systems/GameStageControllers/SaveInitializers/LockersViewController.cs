using System.Collections.Generic;
using UnityEngine;
using YooE.Diploma.Interaction;

namespace YooE.Diploma
{
    public sealed class LockersViewController : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private List<CharacterInteractionComponent> _lockersComponents = new();

        public void OnInit()
        {
            HideView();
        }

        private void HideView()
        {
            DisableLockersInteraction();
        }

        public void EnableLockersInteraction()
        {
            for (var i = 0; i < _lockersComponents.Count; i++)
            {
                _lockersComponents[i].EnableInteractionAbility();
            }
        }

        public void DisableLockersInteraction()
        {
            for (var i = 0; i < _lockersComponents.Count; i++)
            {
                _lockersComponents[i].DisableInteractionAbility();
                _lockersComponents[i].DisableInteractView();
            }
        }
    }
}