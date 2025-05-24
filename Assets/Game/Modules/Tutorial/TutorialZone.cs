using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class TutorialZone : MonoBehaviour
    {
        public event Action OnPlayerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                OnPlayerEnter?.Invoke();
            }
        }
    }
}