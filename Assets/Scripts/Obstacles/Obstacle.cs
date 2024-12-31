using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        audioSource.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !GameManager.Instance.GameFinished)
        {
            ObstacleHit(collision);
            AudioController.Instance.PlaySound(_audioClip, audioSource);
        }
    }

    public abstract void ObstacleHit(Collision collision);

}
