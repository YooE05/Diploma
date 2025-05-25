using UnityEngine;

namespace YooE.Diploma
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset = new Vector3(0, 2, 0);
        [SerializeField] private RectTransform _healthBarRect;
        [SerializeField] private Transform _visualGO;

        public bool NeedCanvasFollow = true;
        private Camera _camera;

        [SerializeField] private SliderView _sliderView;
        [SerializeField] private bool _needHideWhenEmpty = true;
        [SerializeField] private bool _needHideBeforeDamage = true;

        public void Start()
        {
            Debug.Log("HP start " + gameObject.name);
            _camera = Camera.main;
            if (_needHideBeforeDamage)
            {
                gameObject.SetActive(false);
            }
        }

        public void ResetHpBar()
        {
            if (_needHideBeforeDamage)
            {
                gameObject.SetActive(false);
            }

            _sliderView.gameObject.SetActive(true);
        }

        private void Update()
        {
            UpdateSliderPosition();
        }

        private void UpdateSliderPosition()
        {
            if (!NeedCanvasFollow) return;
            if (_healthBarRect == null) return;

            var worldPosition = _visualGO.position + _offset;
            var screenPos = _camera.WorldToScreenPoint(worldPosition);
            _healthBarRect.position = screenPos;
        }

        public void SetNewValue(float newValue)
        {
            if (_sliderView == null) return;
            gameObject.SetActive(true);

            if (newValue <= 0)
            {
                if (_needHideWhenEmpty)
                {
                    _sliderView.gameObject.SetActive(false);
                }

                _sliderView.SetSliderValue(0);
                return;
            }

            _sliderView.SetSliderValue(newValue);
        }
    }
}