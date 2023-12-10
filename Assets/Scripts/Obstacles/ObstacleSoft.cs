using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSoft : Obstacle
{
    private Collider _collider;
    [SerializeField, Range(0f,1f)] private float _slowdownMultiplier;
    [SerializeField] private float _particlePlaybackTime;
    private MeshRenderer _renderer;
    [SerializeField] private GameObject _particles;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void ObstacleHit(Collision collision)
    {
        if (CompareTag("Snowman"))
        {
            GameManager.Instance.killedSnowmans++;
        }

        _collider.enabled = false;
        _renderer.enabled = false;
        _particles.SetActive(true);
        collision.rigidbody.AddForce(-collision.transform.forward * (collision.rigidbody.velocity.magnitude / _slowdownMultiplier));
        
    }
}
