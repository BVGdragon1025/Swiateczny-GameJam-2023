using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //Private variables
    [SerializeField] private float _knockbackForce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SubtractScore();
            collision.rigidbody.AddForce(-collision.transform.forward * (collision.rigidbody.mass * _knockbackForce), ForceMode.Impulse);
            Debug.Log($"Player has hit obstacle! Obstacle: {name}, {transform.position}");
            
        }
    }
}
