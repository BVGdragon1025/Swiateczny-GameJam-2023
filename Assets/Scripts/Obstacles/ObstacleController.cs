using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //Private variables
    [SerializeField] private float _knockbackForce;
    [SerializeField] private AudioClip _clip;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GameManager.Instance.GameFinished)
        {
            if (CompareTag("HardObstacle"))
            {
                collision.rigidbody.AddForce(-collision.transform.forward * (collision.rigidbody.mass * _knockbackForce), ForceMode.Impulse);
                Debug.Log($"Player has hit obstacle! Obstacle: {name}, {transform.position}");
                AudioController.Instance.PlaySound(_clip, _source);
            }
            if (CompareTag("SoftObstacle")) { }
            
            
        }
    }
}
