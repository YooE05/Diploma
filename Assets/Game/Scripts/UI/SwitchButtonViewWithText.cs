using TMPro;
using UnityEngine;

namespace YooE
{
    public class SwitchButtonViewWithText : SwitchButtonView, IHaveTextField
    {
        [SerializeField] private TextMeshProUGUI _buttonText;

        public void SetText(string newText)
        {
            _buttonText.text = newText;
        }
    }
}