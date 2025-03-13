using UnityEngine;

namespace Audio.Content
{
    public sealed class SettingSoundsMusic : MonoBehaviour
    {
        [Range(0.0f, 1.0f)] [SerializeField] private float _musicVolume;

        [SerializeField] private AudioClip _audioClip;

        private void Start()
        {
          //  AudioManager.Instance.PlaySound(_audioClip, AudioOutput.Master);
        }

        [ContextMenu(nameof(Apply))]
        private void Apply()
        {
          //  AudioManager.Instance.SetVolume(AudioOutput.Master, _musicVolume);
        }
    }
}