using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _audioSources = new();

        public void SetSoundsEnabling(bool isEnable)
        {
            for (var i = 0; i < _audioSources.Count; i++)
            {
                _audioSources[i].enabled = isEnable;
            }
        }
    }
}