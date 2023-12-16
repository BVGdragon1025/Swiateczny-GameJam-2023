using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> _axleInfos; // the information about each individual axle
    [SerializeField] private float _maxMotorTorque; // maximum torque the motor can apply to wheel
    [SerializeField] private float _maxBrakingTorque; // maximum torque the motor can apply to wheel
    [SerializeField] private float _maxSteeringAngle; // maximum steer angle the wheel can have
    [SerializeField] private float _maxSpeed; //maximum value of speed, when player is not giving any input. In km/h.
    [SerializeField] private float _maxSpeedAccelerated; //maximum value of speed, when player is giving an input. In km/h.
    [SerializeField] private float _currentSteeringAngle;
    [SerializeField] private float _currentSpeed; //in km/h
    [SerializeField] private TextMeshProUGUI _speedText;
 
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _currentSpeed = _rb.velocity.magnitude * 3.6f;
        Debug.Log($"Speed: {Mathf.RoundToInt(_currentSpeed)}km/h");

        if (Input.GetKeyDown(KeyCode.Backspace) && !GameManager.Instance.GameFinished)
        {
            GameManager.Instance.SpawnOnCheckpoint(gameObject);
        }
        
    }

    public void FixedUpdate()
    {
        bool gameFinished = GameManager.Instance.GameFinished;
        bool gamePaused = GameManager.Instance.GamePaused;
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        float steering = _maxSteeringAngle * horizontalInput;

        foreach (AxleInfo axleInfo in _axleInfos)
        {
            if (axleInfo.steering)
            {
                if (!gameFinished || !gamePaused)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
            }
            if (axleInfo.motor)
            {
                if (!gameFinished || !gamePaused)
                {
                    axleInfo.leftWheel.motorTorque = MoveCharacter(forwardInput);
                    axleInfo.rightWheel.motorTorque = MoveCharacter(forwardInput);
                }

            }
            if (axleInfo.breaking)
            {
                if (!gameFinished)
                {
                    if (!gamePaused)
                    {
                        axleInfo.leftWheel.brakeTorque = Handbrake();
                        axleInfo.rightWheel.brakeTorque = Handbrake();
                    }
                    
                }
                else
                {
                    axleInfo.leftWheel.brakeTorque = _maxBrakingTorque * 4;
                    axleInfo.rightWheel.brakeTorque = _maxBrakingTorque * 4;
                }
                    
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

    float Handbrake()
    {
        if(Input.GetAxis("Jump") != 0)
        {
            return _maxBrakingTorque;
        }
        else
        {
            return 0;
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
    public bool breaking; //is this wheel braking?
}
