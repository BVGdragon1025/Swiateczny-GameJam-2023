using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnflipCar : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private bool _isFlipped;
    [SerializeField] private bool _isUnfliping;
    [SerializeField] private float _unflipTime;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckIfCarFlipped();
    }

    void CheckIfCarFlipped()
    {
        //Debug.Log($"Up Y Rotation: {transform.up.normalized.y}");

        if (transform.up.normalized.y < 0.8f && _rb.velocity.magnitude < 2.0f)
        {

            transform.rotation = Quaternion.Euler(transform.rotation.x, Camera.main.transform.rotation.eulerAngles.y, 0f);

            //Debug.Log($"Car flipped! Up Y Rotation: {transform.up.normalized.y}");
        }

    }


}
