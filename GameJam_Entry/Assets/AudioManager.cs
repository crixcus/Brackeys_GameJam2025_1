using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    public AudioClip bg;
    public AudioClip running;
    public AudioClip lockpick;
    public AudioClip door;
    public AudioClip enemy_detect;
    public AudioClip record1;
    public AudioClip record2;
    public AudioClip record3;
    public AudioClip record4;

    public AudioSource audioSource;

    public void Start()
    {
        bgm.clip = bg;
        bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void PlaySFXLoop(AudioClip clip)
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source.clip != clip)
        {
            source.clip = clip;
            source.loop = true;  // Enable looping
            source.Play();
        }
    }

    public void StopSFX(AudioClip clip)
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            if (source.clip == clip && source.isPlaying)
            {
                source.Stop();
                break;
            }
        }
    }

    public void isPLaying(AudioClip clip)
    {
        
    }
}
