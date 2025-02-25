using UnityEngine;
using UnityEngine.UI;

namespace YooE
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetSliderValue(float newValue)
        {
            _slider.value = newValue;
        }
    }
}