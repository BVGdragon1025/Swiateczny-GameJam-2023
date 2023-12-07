using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> _axleInfos; // the information about each individual axle
    [SerializeField] private float _maxMotorTorque; // maximum torque the motor can apply to wheel
    [SerializeField] private float _maxSteeringAngle; // maximum steer angle the wheel can have
    [SerializeField] private float _maxSpeed; //maximum value of speed, when player is not giving any input. In km/h.
    [SerializeField] private float _maxSpeedAccelerated; //maximum value of speed, when player is giving an input. In km/h.
    [SerializeField] private float _currentSteeringAngle;
    [SerializeField] private float _currentSpeed; //in km/h
 
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _currentSpeed = _rb.velocity.magnitude * 3.6f;
    }

    public void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //float motor = _maxMotorTorque * forwardInput * Time.deltaTime;
        float steering = _maxSteeringAngle * horizontalInput;

        foreach (AxleInfo axleInfo in _axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = MoveCharacter(forwardInput);
                axleInfo.rightWheel.motorTorque = MoveCharacter(forwardInput);
                axleInfo.leftWheel.brakeTorque = ReduceSpeed(forwardInput);
                axleInfo.rightWheel.brakeTorque = ReduceSpeed(forwardInput);
            }
        }
    }

    float MoveCharacter(float vInput)
    {
        float motorTorque;

        if(vInput == 0)
        {
            if(_currentSpeed < _maxSpeed)
            {
                //var multiplier = Mathf.Lerp()
                motorTorque = _maxMotorTorque;
                return motorTorque;
            }
            else
            {
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
            }
        }

        if(vInput != 0)
        {
            if(_currentSpeed < _maxSpeedAccelerated)
            {
                motorTorque = (_maxMotorTorque * 2) * vInput;
                return motorTorque;
            }
            else
            {
                _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeedAccelerated);
            }
            
        }

        return 0;

    }

    void MoveCharacterRB(float vInput)
    {

    }

    float ReduceSpeed(float vInput)
    {
        float brakeTorque;

        if(vInput == 0 && _currentSpeed > _maxSpeed)
        {
            brakeTorque = _maxMotorTorque;
            return brakeTorque;
        }
        else if(vInput != 0 && _currentSpeed > _maxSpeedAccelerated)
        {
            brakeTorque = _maxMotorTorque * 2;
            return brakeTorque;
        }
        else
        {
            brakeTorque = 0;
            return brakeTorque;
        }


    }

}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
