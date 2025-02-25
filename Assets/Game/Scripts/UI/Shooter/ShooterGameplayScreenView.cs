using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class ShooterGameplayScreenView : MonoBehaviour
    {
        public event Action<bool> OnSoundButtonClicked;

        [SerializeField] private SliderWithTextView _enemySlider;
        [SerializeField] private SwitchButtonView _soundsButton;

        private void OnEnable()
        {
            _soundsButton.OnButtonClicked += SoundButtonClicked;
        }

        private void OnDisable()
        {
            _soundsButton.OnButtonClicked -= SoundButtonClicked;
        }

        public void UpdateEnemySlider(float newDefeatPercent)
        {
            _enemySlider.SetSliderValue(newDefeatPercent / 100f);
            _enemySlider.SetSliderText($"{((int)newDefeatPercent).ToString()}%");
        }

        public void SetSoundButtonEnabling(bool isSwitchedOn)
        {
            _soundsButton.SetSwitchPosition(isSwitchedOn);
        }

        private void SoundButtonClicked()
        {
            _soundsButton.Switch();
            OnSoundButtonClicked?.Invoke(_soundsButton.IsSwitchedOn);
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

    public sealed class ShooterGameplayScreenPresenter : Listeners.IFinishListener
    {
        private readonly AudioSystem _audioSystem;
        private readonly ShooterGameplayScreenView _gameplayScreenView;
        private readonly EnemyBrainsInitializer _enemyBrainsInitializer;

        //TODO: add SaveSystem to restore soundButton enabling

        public ShooterGameplayScreenPresenter(AudioSystem audioSystem, ShooterGameplayScreenView gameplayScreenView,
            bool isSoundsOn, EnemyBrainsInitializer enemyBrainsInitializer)
        {
            _audioSystem = audioSystem;
            _gameplayScreenView = gameplayScreenView;
            InitGameplayScreenView(isSoundsOn);
            _enemyBrainsInitializer = enemyBrainsInitializer;

            _enemyBrainsInitializer.OnLiveEnemiesCountChanged += SetEnemySliderValue;
            _gameplayScreenView.OnSoundButtonClicked += _audioSystem.SetSoundsEnabling;
        }

        private void InitGameplayScreenView(bool isSoundsOn)
        {
            _gameplayScreenView.SetSoundButtonEnabling(isSoundsOn);
            _audioSystem.SetSoundsEnabling(isSoundsOn);
        }

        private void SetEnemySliderValue(int deadCount, int allCount)
        {
            _gameplayScreenView.UpdateEnemySlider(deadCount / (float)allCount * 100);
        }

        ~ShooterGameplayScreenPresenter()
        {
            _enemyBrainsInitializer.OnLiveEnemiesCountChanged -= SetEnemySliderValue;
            _gameplayScreenView.OnSoundButtonClicked -= _audioSystem.SetSoundsEnabling;
        }

        public void OnFinish()
        {
            HideScreenView();
        }

        public void HideScreenView()
        {
            _gameplayScreenView.Hide();
        }
    }
}