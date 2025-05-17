using TMPro;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class ShooterGameplayScreenView : MonoBehaviour
    {
        [SerializeField] private bool _isEndless = false;

        [SerializeField] private SliderWithTextView _enemySlider;
        [field: SerializeField] public SoundButtonView SoundButtonView { get; private set; }

        [SerializeField] private SwitchButtonViewWithText _waveNumber;
        [SerializeField] private TextMeshProUGUI _recordScoreText;
        [SerializeField] private TextMeshProUGUI _currentScoreText;

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

        public void ShowWaveNumber(int waveNumber)
        {
            _waveNumber.SetText($"волна \r\n{waveNumber}");
            _waveNumber.Show();
        }

        public void HideWaveNumber()
        {
            _waveNumber.Hide();
        }

        public void SetUpRecordText(int record)
        {
            if (_isEndless)
                _recordScoreText.text = $"{record}";
        }

        public void UpdateScoreText(int totalDefeatEnemiesCount)
        {
            if (_isEndless)
                _currentScoreText.text = $"{totalDefeatEnemiesCount}";
        }
    }
}