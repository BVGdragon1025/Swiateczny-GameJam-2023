using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private Quaternion _carDefaultRotation;
    [SerializeField] private bool _isCrossed;
    private enum CheckpointTypes { Start, Checkpoint, Finish }
    [SerializeField] private CheckpointTypes _type = CheckpointTypes.Start;

    public Quaternion CarRotation { get { return _carDefaultRotation; } }
    // Start is called before the first frame update
    void Start()
    {
        _isCrossed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_isCrossed)
        {
            _isCrossed = true;
            switch (_type)
            {
                case CheckpointTypes.Start:
                case CheckpointTypes.Checkpoint:
                    _carDefaultRotation = other.transform.rotation;
                    GameManager.Instance.GetLastCheckpoint(transform);
                    break;
                case CheckpointTypes.Finish:
                    GameManager.Instance.FinishRace();
                    break;
            }
        }
    }
}
