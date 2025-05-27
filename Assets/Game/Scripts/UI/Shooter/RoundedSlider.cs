using UnityEngine;
using UnityEngine.UI;

namespace YooE
{
    public class RoundedSlider : MonoBehaviour
    {
        public RectTransform fillTransform;

        private float maxWidth;

        void Start()
        {
            var parentRect = fillTransform.parent.GetComponent<RectTransform>();
            maxWidth = parentRect.rect.width;
            GetComponent<Slider>()
                .onValueChanged
                .AddListener(UpdateFill);
            UpdateFill(GetComponent<Slider>().value);
        }

        void UpdateFill(float value)
        {
            var w = Mathf.Lerp(0, maxWidth, value);
            fillTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, w);
        }
    }
}