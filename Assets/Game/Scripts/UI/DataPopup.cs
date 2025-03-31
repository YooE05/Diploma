using UnityEngine;

namespace YooE.Diploma
{
    public sealed class DataPopup : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _parentGO;

        [SerializeField] private GameObject _qualityDataGO;
        [SerializeField] private GameObject _quantitativeDataGO;

        public void OnInit()
        {
            Hide();
        }

        public void Hide()
        {
            _qualityDataGO.SetActive(false);
            _quantitativeDataGO.SetActive(false);
        }

        public void ShowQualityPopup()
        {
            Hide();
            _parentGO.SetActive(true);

            _qualityDataGO.SetActive(true);
        }

        public void ShowQuantitativePopup()
        {
            _quantitativeDataGO.SetActive(true);
        }
    }
}