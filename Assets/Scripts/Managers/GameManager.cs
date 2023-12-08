using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Private variables
    [SerializeField] private int _score;
    [SerializeField] private int _scoreDeduction;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _lastCheckpoint;
    [SerializeField] private float _time;
    [SerializeField] private bool _gameFinished;

    //Public variables
    public int Score { get { return _score; }}
    public int ScoreDeduction {  get { return _scoreDeduction; }}
    public bool GameFinished { get { return _gameFinished; }}
    public static GameManager Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _gameFinished = false;

    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"Gifts: {_score}";
        if (!_gameFinished)
        {
            CountTime();
        }
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
    }

    public void SubtractScore()
    {
        _score -= _scoreDeduction;
        Debug.Log($"Score deducted! -{_scoreDeduction} pts!");
    }

    public void GetLastCheckpoint(Transform checkpointTransform)
    {
        _lastCheckpoint = checkpointTransform;
    }

    public void SpawnOnCheckpoint(GameObject objectToRespawn)
    {
        Rigidbody rb = objectToRespawn.GetComponent<Rigidbody>();
        CheckpointController cc = _lastCheckpoint.gameObject.GetComponent<CheckpointController>();

        objectToRespawn.transform.position = _lastCheckpoint.transform.position;
        objectToRespawn.transform.rotation = cc.CarRotation;
        rb.velocity = Vector3.zero;
    }

    public void FinishRace()
    {
        _gameFinished = true;
        Debug.Log("Race finished!");
        ShowFinishScreen();
    }

    private void ShowFinishScreen()
    {
        float timeInMinutes = Mathf.FloorToInt(_time / 60);
        float timeInSeconds = Mathf.FloorToInt(_time % 60);
        float timeInMiliseconds = (_time % 1) * 1000;

        string giftsCollected;

        if(_score > 0)
        {
            giftsCollected = $"{_score / _score}";
        }
        else{
            giftsCollected = "0";
        }


        string finalTime = string.Format("{0:00}:{1:00}:{2:000}", timeInMinutes, timeInSeconds, timeInMiliseconds);
        Debug.Log($"Your score: {giftsCollected} gifts X 10 = {_score}, Time: {finalTime}");
    }

    private void CountTime()
    {
        _time += Time.deltaTime;
    }

}
