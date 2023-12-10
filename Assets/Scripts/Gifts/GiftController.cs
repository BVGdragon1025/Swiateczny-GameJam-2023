using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int _points;
    [SerializeField] private AudioClip _pickUpSound;

    //Public Variables
    public int Points { get { return _points; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !GameManager.Instance.GameFinished)
        {
            Debug.Log($"Player collected gift! +{_points} pts!");
            GameManager.Instance.AddScore(_points);
            gameObject.SetActive(false);
            AudioController.Instance.PlaySound(_pickUpSound);
        }
    }

}
