using TMPro;
using UnityEngine;

namespace YooE
{
    public sealed class SliderWithTextView : SliderView, IHaveTextField
    {
        [SerializeField] private TextMeshProUGUI _sliderText;

        public void SetText(string newText)
        {
            _sliderText.text = newText;
        }
    }
}