using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int _points;

    //Public Variables
    public int Points { get { return _points; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.GameFinished)
        {
            Debug.Log($"Player collected gift! +{_points} pts!");
            GameManager.Instance.AddScore(_points);
            gameObject.SetActive(false);
        }
    }

}
