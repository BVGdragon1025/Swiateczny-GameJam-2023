using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Private variables
    [SerializeField] private int _score;
    [SerializeField] private int _scoreDeduction;
    [Header("UI Section")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _giftScore;
    [SerializeField] private TextMeshProUGUI _finalTime;
    [SerializeField] private GameObject _victoryScreen;
    [Header("Other section")]
    [SerializeField] private Transform _lastCheckpoint;
    [SerializeField] private float _time;
    [SerializeField] private bool _gameFinished;
    [SerializeField] private float _distanceToFinish;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _startingLine;
    [SerializeField] private GameObject _finishLine;
    [SerializeField] private Slider _progressBar;


    //Public variables
    public int Score { get { return _score; }}
    public int ScoreDeduction {  get { return _scoreDeduction; }}
    public bool GameFinished { get { return _gameFinished; }}
    public float DistanceLeft { get { return _distanceToFinish; }}
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
            CalculateDistance();
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
        _giftScore.text = $"Prezenty: {giftsCollected} X 10 = {_score}";
        _finalTime.text = $"Czas przejazdu: {finalTime}";
        _victoryScreen.SetActive(true);

        Debug.Log($"Your score: {giftsCollected} gifts X 10 = {_score}, Time: {finalTime}");
    }

    private void CountTime()
    {
        _time += Time.deltaTime;
    }

    private void CalculateDistance()
    {
        //_progressBar.minValue = Vector3.Distance(_player.transform.position, _startingLine.transform.position);
        _progressBar.maxValue = Vector3.Distance(_startingLine.transform.position, _finishLine.transform.position);
        _distanceToFinish = Vector3.Distance(_finishLine.transform.position, _player.transform.position);
        _progressBar.value =  _distanceToFinish;
        Debug.Log(_progressBar.value);

    }

}
