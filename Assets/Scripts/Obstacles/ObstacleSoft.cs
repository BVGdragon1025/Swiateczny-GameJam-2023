using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSoft : Obstacle
{
    [SerializeField] private Collider _collider;
    [SerializeField, Range(0f,1f)] private float _slowdownMultiplier;
    [SerializeField] private float _particlePlaybackTime;
    private MeshRenderer _renderer;
    private ParticleSystem _particles;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<MeshRenderer>();
        _particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void ObstacleHit(Collision collision)
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        _particles.Play();
        collision.rigidbody.AddForce(-collision.transform.forward * (collision.rigidbody.velocity.magnitude / _slowdownMultiplier));
        
    }
}
