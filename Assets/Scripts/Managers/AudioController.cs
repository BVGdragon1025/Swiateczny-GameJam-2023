using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public float musicVolume;
    public float sfxVolume;

    public static AudioController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip, AudioSource audioSource)
    {
        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        }
        else
        {
            sfxVolume = 1;
        }

        audioSource.PlayOneShot(audioClip, sfxVolume);
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        }
        else
        {
            sfxVolume = 1;
        }

        _audioSource.PlayOneShot(audioClip, sfxVolume);
    }

    public void PlayMusic(AudioClip audioClip, AudioSource audioSource)
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }   
        else
        {   
            musicVolume = 0.5f;
        }

        audioSource.PlayOneShot(audioClip, musicVolume);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            musicVolume = 0.5f;
        }

        _audioSource.PlayOneShot(audioClip, musicVolume);
    }

}
