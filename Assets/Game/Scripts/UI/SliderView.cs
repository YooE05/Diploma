using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YooE
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _initValue = 1f;

        private float _targetValue;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            SetSliderValueNoAnimation(_initValue);
            _targetValue = _initValue;
        }

        public void SetSliderValue(float newValue)
        {
            _targetValue = newValue;

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            AnimateFill(_cancellationTokenSource.Token).Forget();
        }

        public void SetSliderValueNoAnimation(float newValue)
        {
            _slider.value = newValue;
        }

        /*private void Update()
        {
            _slider.value = Mathf.Lerp(
                _slider.value,
                _targetValue,
                _speed * Time.deltaTime
            );
        }*/

        private async UniTaskVoid AnimateFill(CancellationToken cancellationToken)
        {
            var start = _slider.value;
            var elapsed = 0f;
            while (elapsed < _duration)
            {
                elapsed += Time.deltaTime;
                _slider.value = Mathf.Lerp(start, _targetValue, elapsed / _duration);
                await UniTask.WaitForEndOfFrame(cancellationToken);
            }

            _slider.value = _targetValue;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}