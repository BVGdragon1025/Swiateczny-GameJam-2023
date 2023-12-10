using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] private WheelCollider[] _wheelColliders = new WheelCollider[4];
    [SerializeField] private GameObject _leftParticleSystem;
    [SerializeField] private GameObject _rightParticleSystem;
    [SerializeField] private int _wheelsOnGround;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            _wheelColliders[i] = transform.GetChild(i).GetComponent<WheelCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleParticles();
        CheckWheels();
    
    }

    void ToggleParticles()
    {
        foreach(var wheel in _wheelColliders)
        {
            if((wheel.name == "frontLeft" && wheel.isGrounded) && (wheel.name == "rearLeft" && wheel.isGrounded))
            {
                _leftParticleSystem.SetActive(true);
            }
            else
            {
                _leftParticleSystem.SetActive(false);
            }

            if ((wheel.name == "frontRight" && wheel.isGrounded) && (wheel.name == "rearRight" && wheel.isGrounded))
            {
                _rightParticleSystem.SetActive(true);
            }
            else
            {
                _rightParticleSystem.SetActive(false);
            }
        }
    }
    
    bool CheckWheels()
    {
        foreach(var wheel in _wheelColliders)
        {
            if(wheel.isGrounded)
            {
                _wheelsOnGround++;
            }
            else
            {
                _wheelsOnGround--;
            }
        }

        if(_wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
