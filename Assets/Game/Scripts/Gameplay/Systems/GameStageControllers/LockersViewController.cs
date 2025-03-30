using UnityEngine;

namespace YooE.Diploma
{
    public sealed class LockersViewController : MonoBehaviour, Listeners.IInitListener
    {
        public void OnInit()
        {
            HideView();
        }

        private void HideView()
        {
       
        }

        public void EnableLockersInteraction()
        {
            throw new System.NotImplementedException();
        }
    }
}