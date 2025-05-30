using DG.Tweening;
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

        private float _showPopupTime = 0.4f;
        private float _showButtonTime = 0.4f;
        private float _buttonsAnimationDelay = 0.4f;


        public void OnInit()
        {
            ResetAllImage();
            _popupGO.SetActive(false);
        }

        [Button]
        public void Hide()
        {
            _popupGO.SetActive(false);
            _popupGO.transform.DOScale(0, _showPopupTime / 2f).SetEase(Ease.InOutSine).SetLink(_popupGO).Play();
        }

        [Button]
        public void Show()
        {
            if (_popupGO.activeSelf)
            {
                _buttonsAnimationDelay = 0f;
                return;
            }

            _buttonsAnimationDelay = _showPopupTime;
            _popupGO.SetActive(true);
            _popupGO.transform.DOScale(1, _showPopupTime).From(0).SetEase(Ease.InOutSine).SetLink(_popupGO).Play();
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
            _observationImage.transform.DOScale(1.1f, _showButtonTime).From(1).SetLink(_hypothesisImage.gameObject)
                .Play().SetDelay(_buttonsAnimationDelay);
            _observationImage.transform.DOScale(1f, _showButtonTime).SetDelay(_buttonsAnimationDelay + _showButtonTime)
                .SetLink(_observationImage.gameObject).Play();
        }

        [Button]
        public void EnableHypothesis()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _hypothesisImage.transform.DOScale(1.1f, _showButtonTime).From(1).SetLink(_hypothesisImage.gameObject)
                .Play().SetDelay(_buttonsAnimationDelay);
            _hypothesisImage.transform.DOScale(1f, _showButtonTime).SetDelay(_buttonsAnimationDelay + _showButtonTime)
                .SetLink(_hypothesisImage.gameObject).Play();
        }

        [Button]
        public void EnableExperiment()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _experimentImage.sprite = _enableSprite;
            _experimentImage.transform.DOScale(1.1f, _showButtonTime).From(1).SetLink(_analysisImage.gameObject).Play()
                .SetDelay(_buttonsAnimationDelay);
            _experimentImage.transform.DOScale(1f, _showButtonTime).SetDelay(_buttonsAnimationDelay + _showButtonTime)
                .SetLink(_experimentImage.gameObject).Play();
        }

        [Button]
        public void EnableAnalysis()
        {
            ResetAllImage();
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _experimentImage.sprite = _enableSprite;
            _analysisImage.sprite = _enableSprite;
            _analysisImage.transform.DOScale(1.1f, _showButtonTime).From(1).SetLink(_analysisImage.gameObject).Play()
                .SetDelay(_buttonsAnimationDelay);
            _analysisImage.transform.DOScale(1f, _showButtonTime).SetDelay(_buttonsAnimationDelay + _showButtonTime)
                .SetLink(_analysisImage.gameObject).Play();
        }

        [Button]
        public void EnableConclusion()
        {
            _observationImage.sprite = _enableSprite;
            _hypothesisImage.sprite = _enableSprite;
            _experimentImage.sprite = _enableSprite;
            _analysisImage.sprite = _enableSprite;
            _conclusionImage.sprite = _enableSprite;
            _conclusionImage.transform.DOScale(1.1f, _showButtonTime).From(1).SetLink(_conclusionImage.gameObject)
                .Play().SetDelay(_buttonsAnimationDelay);
            _conclusionImage.transform.DOScale(1f, _showButtonTime).SetDelay(_buttonsAnimationDelay + _showButtonTime)
                .SetLink(_conclusionImage.gameObject).Play();
        }
    }
}