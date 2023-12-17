using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPauser : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;


    private void OnEnable()
    {
        _audioSource.Pause();
    }

    private void OnDisable()
    {
        if(!_audioSource.isPlaying)
            _audioSource.Play();
    }

}
