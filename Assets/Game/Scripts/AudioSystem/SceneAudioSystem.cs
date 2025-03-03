using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace YooE.Diploma
{
    public class SceneAudioSystem : MonoBehaviour, Listeners.IInitListener
    {
        [SerializeField] private List<AudioSource> _audioSources = new();

        public BoolReactiveProperty IsSoundEnable { get; private set; } = new();
        private AudioManager _audioManager;

        [Inject]
        public void Construct(AudioManager audioManager)
        {
            _audioManager = audioManager;
            SetSoundsEnabling(_audioManager.IsSoundEnable.Value);
            _audioManager.IsSoundEnable.Subscribe(delegate { SetSoundsEnabling(audioManager.IsSoundEnable.Value); })
                .AddTo(this);
        }

        public void OnInit()
        {
            _audioSources.AddRange(FindObjectsOfType<AudioSource>());
        }

        public void SetSoundsEnabling(bool isEnable)
        {
            IsSoundEnable.Value = isEnable;
            _audioManager.SetSoundsEnabling(isEnable);
            for (var i = 0; i < _audioSources.Count; i++)
            {
                _audioSources[i].mute = !isEnable;
            }
        }
    }
}