using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAud : Singleton<ControlAud>
{
    [Header("Main Control: ")]
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float sfxVolume;


    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Audio and Music Control: ")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;

    public AudioClip[] bgmusics;

    public override void Start()
    {
        PlayMusic(bgmusics);
    }

    public void PlaySound(AudioClip sound, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }
        aus.PlayOneShot(sound, sfxVolume);

    }

    public void PlaySound(AudioClip[] sounds, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }
        int rand = Random.Range(0, sounds.Length);
        if (sounds[rand] != null)
        {
            aus.PlayOneShot(sounds[rand], sfxVolume);
        }
      
    }


    public void PlayMusic(AudioClip music, bool loop = true)
    {
        if (musicAus)
        {
            musicAus.clip = music;
            musicAus.volume = musicVolume;
            musicAus.loop = loop;
            musicAus.Play();
        }
    }

    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus)
        {
            int rand = Random.Range(0, musics.Length);
            if (musics[rand] != null)
            {
                musicAus.clip = musics[rand];
                musicAus.volume = musicVolume;
                musicAus.loop = loop;
                musicAus.Play();
            }
        }
    }

}
