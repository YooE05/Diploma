using UnityEngine;

namespace YooE.Diploma
{
    public sealed class ShooterGameplayScreenView : MonoBehaviour
    {
        [SerializeField] private SliderWithTextView _enemySlider;
        [field: SerializeField] public SoundButtonView SoundButtonView { get; private set; }

        public void UpdateEnemySlider(float newDefeatPercent)
        {
            _enemySlider.SetSliderValue(newDefeatPercent / 100f);
            _enemySlider.SetText($"{((int)newDefeatPercent).ToString()}%");
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}