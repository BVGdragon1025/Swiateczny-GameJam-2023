using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void PlayMusic(AudioClip audioClip, AudioSource audioSource)
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }   
        else
        {   
            musicVolume = 1;
        }

        audioSource.PlayOneShot(audioClip, musicVolume);
    }

}
