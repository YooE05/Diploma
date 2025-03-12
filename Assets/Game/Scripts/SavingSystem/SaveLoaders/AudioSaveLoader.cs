namespace YooE.SaveLoad
{
    public sealed class AudioSaveLoader : DataSaveLoader<AudioSettingsData, AudioManager>
    {
        protected override AudioSettingsData GetData(AudioManager service)
        {
            return service.GetAudioSettings();
        }

        protected override void SetData(AudioManager audioSystem, AudioSettingsData data)
        {
            audioSystem.SetAudioSettings(data);
        }

        protected override void SetDefaultData(AudioManager audioSystem)
        {
            var data = new AudioSettingsData()
            {
                IsSoundOn = true,
                MasterVolume = 0.5f,
            };

            audioSystem.SetAudioSettings(data);
        }
    }
}