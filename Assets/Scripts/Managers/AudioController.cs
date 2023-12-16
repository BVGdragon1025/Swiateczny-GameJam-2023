using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioSource _sfxSource;

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

    private void Start()
    {
        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            SetSFXVolume(sfxVolume);
        }
        else
        {
            PlayerPrefs.SetFloat("SfxVolume", 1f);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(musicVolume);
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }


    }

    private void Update()
    {
        _audioSource.volume = musicVolume;

        if (_sfxSource != null)
        {
            _sfxSource.volume = sfxVolume;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        //PlaySound(_audioSource.clip);
    }

    public void GetMusicVolume(Slider slider)
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void GetSFXVolume(Slider slider)
    { 
        slider.value = PlayerPrefs.GetFloat("SfxVolume");
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

        if(_sfxSource != null)
        {
            _sfxSource.PlayOneShot(audioClip, sfxVolume);
        }
        else
        {
            _audioSource.PlayOneShot(audioClip, sfxVolume);
        }

        
    }

    public void PlaySound(AudioClip audioClip, AudioSource audioSource, float volumeDifference)
    {
        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        }
        else
        {
            sfxVolume = 1;
        }

        audioSource.PlayOneShot(audioClip, Mathf.Clamp(sfxVolume + volumeDifference, 0.15f, 1f));
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
