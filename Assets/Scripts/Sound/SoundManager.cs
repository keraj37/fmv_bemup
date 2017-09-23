using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public AudioSource audioSource;
    public AudioSource audioSourceMusic;

    void Awake()
    {
        instance = this;
    }

    public static void SetMusicVolume(float volume)
    {
        instance.audioSourceMusic.volume = volume;
    }

    public static void PlaySound(SoundCombo[] soundCombos, float volume = 1f)
    {
        SoundCombo sc = soundCombos[Random.Range(0, soundCombos.Length)];

        foreach(var clip in sc.sounds)
            instance.audioSource.PlayOneShot(clip, volume);
    }
}
