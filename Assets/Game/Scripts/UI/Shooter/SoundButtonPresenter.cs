using System;
using Audio;
using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class SoundButtonPresenter : IDisposable
    {
        private readonly AudioManager _audioManager;
        private readonly SoundButtonView _soundButtonView;
        private readonly string _clipName;
        private readonly SaveLoadManager _saveLoadManager;

        public SoundButtonPresenter(AudioManager audioManager, SoundButtonView soundButtonView, string buttonClipName,
            SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
            _clipName = buttonClipName;
            _soundButtonView = soundButtonView;
            _audioManager = audioManager;
            _audioManager.OnNewSettingsSet += InitGameplayScreenView;
            InitGameplayScreenView(_audioManager.GetAudioSettings());

            _soundButtonView.OnSoundButtonClicked += OnSoundButtonClicked;
        }

        private void InitGameplayScreenView(AudioSettingsData audioSettings)
        {
            _soundButtonView.SetSoundButtonEnabling(audioSettings.IsSoundOn);
        }

        private void OnSoundButtonClicked(bool isEnable)
        {
            if (_audioManager.TryGetAudioClipByName(_clipName, out var audioClip))
            {
                _audioManager.PlaySound(audioClip, AudioOutput.UI);
            }

            _audioManager.SetSoundsEnabling(isEnable);
            _saveLoadManager.SaveGame();
        }

        public void HideSoundButton()
        {
            _soundButtonView.Hide();
        }

        public void Dispose()
        {
            _audioManager.OnNewSettingsSet -= InitGameplayScreenView;
            _soundButtonView.OnSoundButtonClicked -= OnSoundButtonClicked; //_audioManager.SetSoundsEnabling;
        }
    }
}