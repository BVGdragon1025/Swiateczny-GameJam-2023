using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private Quaternion _carDefaultRotation;
    [SerializeField] private bool _isCrossed;
    private enum CheckpointTypes { Start, Checkpoint, Finish }
    [SerializeField] private CheckpointTypes _type = CheckpointTypes.Start;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    public Quaternion CarRotation { get { return _carDefaultRotation; } }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

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
                    GameManager.Instance.GetLastCheckpoint(transform);
                    break;
                case CheckpointTypes.Checkpoint:
                    GameManager.Instance.GetLastCheckpoint(transform);
                    AudioController.Instance.PlaySound(_audioClip, _audioSource);
                    break;
                case CheckpointTypes.Finish:
                    GameManager.Instance.FinishRace();
                    break;
            }
        }
    }
}
