using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private AudioSlider[] audioSliders;

    private AudioManager _audioManager;

    [Inject]
    public void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    private void Start()
    {
        foreach (AudioSlider audioSlider in audioSliders)
        {
            audioSlider.audioMixerGroup.audioMixer.GetFloat(audioSlider.audioMixerGroup.name, out float value);

            audioSlider.slider.value = Mathf.Pow(10, (value / 20));
            audioSlider.slider.onValueChanged.AddListener(delegate
            {
                _audioManager.ChangeVolume(audioSlider.audioMixerGroup.name, audioSlider.slider.value);
            });
        }
    }
}

[System.Serializable]
public class AudioSlider
{
    public Slider slider;
    public AudioMixerGroup audioMixerGroup;
}