using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHard : Obstacle
{
    [SerializeField] private float _knockbackForce;

    public override void ObstacleHit(Collision collision)
    {
        collision.rigidbody.AddForce(-collision.transform.forward * (collision.rigidbody.mass * _knockbackForce), ForceMode.Impulse);
    }

}
