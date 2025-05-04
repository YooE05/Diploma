using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace YooE
{
    public sealed class SliderWithTextView : SliderView, IHaveTextField
    {
        [SerializeField] private List<TextMeshProUGUI> _sliderText;

        public void SetText(string newText)
        {
            for (var i = 0; i < _sliderText.Count; i++)
            {
                _sliderText[i].text = newText;
            }
        }
    }
}