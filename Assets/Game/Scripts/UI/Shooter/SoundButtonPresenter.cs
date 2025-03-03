using UniRx;

namespace YooE.Diploma
{
    public sealed class SoundButtonPresenter
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly SceneAudioSystem _sceneAudioSystem;
        private readonly SoundButtonView _soundButtonView;

        public SoundButtonPresenter(SceneAudioSystem sceneAudioSystem, SoundButtonView soundButtonView)
        {
            _soundButtonView = soundButtonView;
            _sceneAudioSystem = sceneAudioSystem;
            InitGameplayScreenView(sceneAudioSystem.IsSoundEnable.Value);
            sceneAudioSystem.IsSoundEnable.Subscribe(delegate
                {
                    InitGameplayScreenView(sceneAudioSystem.IsSoundEnable.Value);
                })
                .AddTo(_disposables);

            _soundButtonView.OnSoundButtonClicked += _sceneAudioSystem.SetSoundsEnabling;
        }

        private void InitGameplayScreenView(bool isSoundsOn)
        {
            _soundButtonView.SetSoundButtonEnabling(isSoundsOn);
        }

        public void HideSoundButton()
        {
            _soundButtonView.Hide();
        }

        ~SoundButtonPresenter()
        {
            _soundButtonView.OnSoundButtonClicked -= _sceneAudioSystem.SetSoundsEnabling;
            _disposables.Dispose();
        }
    }
}