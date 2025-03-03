using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;

    private List<AudioSource> activeAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip != null)
        {

            AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
            activeAudioSources.Add(audioSource);
            float clipLength = audioSource.clip.length;
            StartCoroutine(DestroyAudioSourceAfterPlay(audioSource, clipLength));
        }
        else
        {
            Debug.Log("Переданный аудиоклип равен NULL");
        }
    }

    public void PlayRandomSoundClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip.Length > 0)
        {
            int rand = Random.Range(0, audioClip.Length);
            AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = audioClip[rand];
            audioSource.volume = volume;
            audioSource.Play();
            activeAudioSources.Add(audioSource);
            float clipLength = audioSource.clip.length;
            StartCoroutine(DestroyAudioSourceAfterPlay(audioSource, clipLength));
        }
        else
        { Debug.Log("В SoundManager не передано звуков для воспроизведения"); }
    }

    private IEnumerator DestroyAudioSourceAfterPlay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null)
        {
            activeAudioSources.Remove(audioSource);
            Destroy(audioSource.gameObject);
        }
    }

    public void StopAllSounds()
    {
        foreach (var audioSource in activeAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
                Destroy(audioSource.gameObject);
            }
        }
        activeAudioSources.Clear();
    }

    public void StopSound(AudioSource audioSource)
    {
        if (audioSource != null && activeAudioSources.Contains(audioSource))
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
            activeAudioSources.Remove(audioSource);
        }
    }
}