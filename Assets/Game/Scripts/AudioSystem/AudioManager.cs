using UnityEngine.Audio;
using System;
using UniRx;
using UnityEngine;
using YooE.SaveLoad;


[CreateAssetMenu(fileName = "AudioManager", menuName = "AudioSystem/AudioManager", order = 1)]
public sealed class AudioManager : ScriptableObject
{
    public static GameObject Instance { get; private set; }

    [SerializeField] private SoundsRefsSO _soundsRefsSO;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _masterAudioMixerGroup;

    public BoolReactiveProperty IsSoundEnable { get; private set; } = new();

    public void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = GameObject.Instantiate(new GameObject());
        }
        else
        {
            // Destroy(gameObject);
            return;
        }

        GameObject.DontDestroyOnLoad(Instance);

        foreach (var sound in _soundsRefsSO.sounds)
        {
            sound.source = Instance.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            sound.source.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    private Sound FindSound(string name, Sound[] sounds)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return null;
        }

        return sound;
    }

    private Sound FindSound(string name)
    {
        Sound sound = Array.Find(_soundsRefsSO.sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return null;
        }

        return sound;
    }

    public void PlaySound(string name)
    {
        FindSound(name).source.Play();
    }

    public void StopSound(string name)
    {
        FindSound(name).source.Stop();
    }

    public void StopSounds()
    {
        foreach (Sound sound in _soundsRefsSO.sounds)
        {
            sound.source.Stop();
        }
    }

    public void StopSounds(string[] exeptions)
    {
        foreach (Sound sound in _soundsRefsSO.sounds)
        {
            foreach (string name in exeptions)
            {
                if (sound.name != name)
                {
                    sound.source.Stop();
                }
            }
        }
    }

    public void ChangeVolume(string mixerGroupName, float value)
    {
        value = value == 0 ? Mathf.Epsilon : value;

        _audioMixer.SetFloat(mixerGroupName, Mathf.Log10(value) * 20);
    }

    public float ClipLength(string name)
    {
        return FindSound(name).clip.length;
    }

    public void SetAudioSettings(AudioSettingsData settings)
    {
        ChangeVolume(_masterAudioMixerGroup.name, settings.MasterVolume);
        SetSoundsEnabling(settings.IsSoundOn);
    }

    public void SetSoundsEnabling(bool settingsIsSoundOn)
    {
        IsSoundEnable.Value = settingsIsSoundOn;
        ChangeVolume(_masterAudioMixerGroup.name, settingsIsSoundOn ? 0.4f : 0.000001f);
    }

    public AudioSettingsData GetAudioSettings()
    {
        var data = new AudioSettingsData()
        {
            IsSoundOn = IsSoundEnable.Value,
            MasterVolume = 0.3f
        };
        return data;
    }
}