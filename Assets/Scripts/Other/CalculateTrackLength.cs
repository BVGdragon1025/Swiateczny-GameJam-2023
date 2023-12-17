using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CalculateTrackLength : MonoBehaviour
{
    [SerializeField] private List<GameObject> _points;
    [SerializeField] private float _distance;

    private void Awake()
    {
        _distance = 0f;

        for(int i = 0; i < _points.Count - 1; i++)
        {
            //_distance += (_points[i + 1].transform.position - _points[i].transform.position).magnitude;
            _distance += Mathf.Sqrt(Mathf.Pow((_points[i + 1].transform.position.x - _points[i].transform.position.x),2) + Mathf.Pow((_points[i + 1].transform.position.z - _points[i].transform.position.z),2));
        }
        
    }

    void Start()
    {
        GameManager.Instance.DistanceLeft = _distance;
    }

}
