using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace YooE.Diploma
{
    public sealed class ScienceMethodPopup : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private GameObject _popupGO;

        [SerializeField] private Image _observationImage;
        [SerializeField] private Image _hypothesisImage;
        [SerializeField] private Image _experimentImage;
        [SerializeField] private Image _analysisImage;
        [SerializeField] private Image _conclusionImage;

        [SerializeField] private Sprite _disableSprite;
        [SerializeField] private Sprite _enableSprite;

        public void OnInit()
        {
            ResetAllImage();
            Hide();
        }

        [Button]
        public void Hide()
        {
            _popupGO.SetActive(false);
        }

        [Button]
        public void Show()
        {
            _popupGO.SetActive(true);
        }

        [Button]
        public void ResetAllImage()
        {
            _observationImage.sprite = _disableSprite;
            _hypothesisImage.sprite = _disableSprite;
            _experimentImage.sprite = _disableSprite;
            _analysisImage.sprite = _disableSprite;
            _conclusionImage.sprite = _disableSprite;
        }

        [Button]
        public void EnableObservation()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
        }

        [Button]
        public void EnableHypothesis()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
        }

        [Button]
        public void EnableExperiment()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _experimentImage.sprite = _enableSprite;
        }

        [Button]
        public void EnableAnalysis()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _experimentImage.sprite = _enableSprite;
            _analysisImage.sprite = _enableSprite;
        }

        [Button]
        public void EnableConclusion()
        {
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _experimentImage.sprite = _enableSprite;
            _analysisImage.sprite = _enableSprite;
            _conclusionImage.sprite = _enableSprite;
        }
    }
}