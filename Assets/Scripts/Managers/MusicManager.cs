using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioController))]

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musicList;
    private AudioController _controller;
    private AudioSource _audioSource;

    private void Awake()
    {
        _controller = GetComponent<AudioController>();
        _audioSource = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        ChooseRandomSong();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_audioSource.isPlaying)
        {
            ChooseRandomSong();
        }

    }

    void ChooseRandomSong()
    {
        //int songSelection = 0;
        //Debug.Log($"Now rolled: {songSelection}");
        var songSelection = new System.Random();
        int randomVar = songSelection.Next(0, _musicList.Count);
        
        Debug.Log($"Now rolled: {randomVar}");

        _controller.PlayMusic(_musicList[randomVar], _audioSource);
        Debug.Log($"Now playing: {_musicList[randomVar].name}, Amount of songs: {_musicList.Count}, Random number: {randomVar}");

    }



}
