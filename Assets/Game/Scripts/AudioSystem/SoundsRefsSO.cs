using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundsRefsSO", menuName = "AudioSystem/SoundsRefsSO")]
public class SoundsRefsSO : ScriptableObject
{
    public Sound[] sounds;

    public Sound FindSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
}