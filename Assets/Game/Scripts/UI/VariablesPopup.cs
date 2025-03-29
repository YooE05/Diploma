using UnityEngine;

namespace YooE.Diploma
{
    public sealed class VariablesPopup : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _parentGO;

        [SerializeField] private GameObject _independentVarGO;
        [SerializeField] private GameObject _dependentVarGO;

        public void OnInit()
        {
            Hide();
        }

        public void Hide()
        {
            _independentVarGO.SetActive(false);
            _dependentVarGO.SetActive(false);
        }

        public void ShowIndependentPopup()
        {
            Hide();
            _parentGO.SetActive(true);

            _independentVarGO.SetActive(true);
        }

        public void ShowDependentPopup()
        {
            _dependentVarGO.SetActive(true);
        }
    }
}