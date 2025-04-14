using UnityEngine;

namespace YooE.Diploma
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset = new Vector3(0, 2, 0);
        [SerializeField] private RectTransform _healthBarRect;
        [SerializeField] private Transform _visualGO;

        public bool NeedShowHp;
        private Camera _camera;

        [SerializeField] private SliderView _sliderView;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!NeedShowHp) return;
            if (_healthBarRect == null) return;

            var worldPosition = _visualGO.position + _offset;
            var screenPos = _camera.WorldToScreenPoint(worldPosition);
            _healthBarRect.position = screenPos;
        }

        public void SetNewValue(float newValue)
        {
            Debug.Log(newValue);

            if (_sliderView == null) return;
            if (newValue <= 0)
            {
                _sliderView.gameObject.SetActive(false);
                return;
            }

            _sliderView.SetSliderValue(newValue);
        }
    }
}