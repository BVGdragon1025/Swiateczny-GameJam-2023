using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Private variables
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _defaultAccSpeed;
    [SerializeField] private float _accelerationRate;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _currentSpeed;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        MoveCharacter(forwardInput);
        RotateCharacter(horizontalInput);

    }

    void MoveCharacter(float vInput)
    {
        Vector3 movement = transform.forward * _accelerationRate;
        Vector3 movementAcc = transform.forward * _accelerationRate * 2;
        Debug.Log(vInput);

        _currentSpeed = _rb.velocity.magnitude;

        if (vInput == 0f)
        {
            if (_currentSpeed > _defaultSpeed)
            {
                _rb.AddRelativeForce(-transform.forward * _accelerationRate);
            }
            else
            {
                _rb.AddRelativeForce(movement);
            }
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _defaultSpeed);

        }
        else if (vInput != 0)
        {
            if(vInput < 0f)
            {
                //Mathf.Clamp(_rb.velocity.magnitude, 1f, 1f);
                _rb.AddRelativeForce(vInput * 0.1f * -transform.forward);
                Mathf.Clamp(_rb.velocity.magnitude, _minSpeed, _minSpeed);
            }
            else
            {
                _rb.AddRelativeForce(movementAcc * vInput);
                //Mathf.Clamp(_rb.velocity.magnitude, 0f, _defaultAccSpeed);
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _defaultAccSpeed);
            }

        }

    }

    void RotateCharacter(float hInput)
    {
        transform.Rotate(Vector3.up, hInput * _turnSpeed * Time.deltaTime);
    }

}
