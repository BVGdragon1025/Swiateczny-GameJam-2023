using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] private WheelCollider[] _wheelColliders = new WheelCollider[4];
    [SerializeField] private GameObject _leftParticleSystem;
    [SerializeField] private GameObject _rightParticleSystem;
    public int _wheelsOnGround;

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
        CheckWheels();
        //ToggleParticles();

    }

    void ToggleParticles()
    {
        foreach(var wheel in _wheelColliders)
        {

            if((wheel.name == "frontLeft" || wheel.name == "rearLeft") && wheel.isGrounded)
            {
                _leftParticleSystem.SetActive(true);
                Debug.Log($"Left particle: on");
            }
            else
            {
                _leftParticleSystem.SetActive(false);
                Debug.Log($"Left particle: off");
            }

            if ((wheel.name == "frontRight" || wheel.name == "rearRight") && wheel.isGrounded)
            {
                _rightParticleSystem.SetActive(true);
                Debug.Log($"Right particle: on");
            }
            else
            {
                _rightParticleSystem.SetActive(false);
                Debug.Log($"Right particle: off");
            }
        }
    }
    
    bool CheckWheels()
    {
        _wheelsOnGround = 0;

        foreach(var wheel in _wheelColliders)
        {
            if(wheel.isGrounded)
            {
                _wheelsOnGround++;
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
