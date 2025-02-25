using TMPro;
using UnityEngine;

namespace YooE
{
    public sealed class SliderWithTextView : SliderView
    {
        [SerializeField] private TextMeshProUGUI _sliderText;

        public void SetSliderText(string newSliderText)
        {
            _sliderText.text = newSliderText;
        }
    }
}