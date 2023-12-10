using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioController))]

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musicList;
    private AudioController _controller;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<AudioController>();
        _audioSource = GetComponent<AudioSource>();
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
        int songSelection;

        songSelection = Random.Range(0, _musicList.Count);

        _controller.PlayMusic(_musicList[songSelection], _audioSource);
        Debug.Log($"Now playing: {_musicList[songSelection]}");

    }



}
