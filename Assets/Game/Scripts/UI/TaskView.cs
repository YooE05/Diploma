using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YooE.Diploma
{
    public sealed class TaskView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _completedSprite;
        [SerializeField] private Sprite _uncompletedSprite;

        public void Show(string text)
        {
            _icon.sprite = _uncompletedSprite;
            _text.text = text;
            _text.fontStyle &= ~FontStyles.Strikethrough;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool TryComplete(string needText)
        {
            if (needText == _text.text)
            {
                _icon.sprite = _completedSprite;
                _text.fontStyle = FontStyles.Strikethrough;
                return true;
            }

            return false;
        }
    }
}