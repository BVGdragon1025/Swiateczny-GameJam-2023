using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Private variables
    [SerializeField] private int _gifts;
    [SerializeField] private int _score;
    [SerializeField] private int _scoreDeduction;
    [SerializeField] public int killedSnowmans;
    [Header("UI Section")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _deadSnowmanText;
    [SerializeField] private TextMeshProUGUI _giftScore;
    [SerializeField] private TextMeshProUGUI _finalTime;
    [SerializeField] private TextMeshProUGUI _finalScore;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _scoreScreen;
    [SerializeField] private GameObject _goodJobScreen;
    [SerializeField] private GameObject _badJobScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private float _timeToShowResults;
    [Header("Other section")]
    [SerializeField] private Transform _lastCheckpoint;
    [SerializeField] private float _time;
    [SerializeField] private float _targetTime;
    [SerializeField] private bool _gameFinished;
    [SerializeField] private bool _isPaused;
    [SerializeField] private float _distanceToFinish;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _startingLine;
    [SerializeField] private GameObject _finishLine;
    [SerializeField] private Slider _progressBar;
    [Header("Fireworks")]
    [SerializeField] private List<GameObject> _fireworks;
    [SerializeField] private AudioClip _fireworksSound;


    //Public variables
    public int Score { get { return _score; }}
    public int ScoreDeduction {  get { return _scoreDeduction; }}
    public bool GameFinished { get { return _gameFinished; }}
    public bool GamePaused { get { return _isPaused; }}
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

        Time.timeScale = 1;
        _gameFinished = false;
        _isPaused = false;

    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"{_gifts}";
        _deadSnowmanText.text = $"{killedSnowmans}";
        if (!_gameFinished)
        {
            CountTime();
            CalculateDistance();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !_isPaused && !_gameFinished)
        {
            PauseMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _isPaused && !_gameFinished)
        {
            ContinueGame();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && _gameFinished)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();   
        }

    }

    public void AddScore(int scoreToAdd)
    {
        _gifts++;
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
        for(int i = 0; i < _fireworks.Count; i++)
        {
            _fireworks[i].SetActive(true);
            AudioController.Instance.PlaySound(_fireworksSound);
        }

        ShowFinishScreen();
    }

    private void ShowFinishScreen()
    {
        float timeInMinutes = Mathf.FloorToInt(_time / 60);
        float timeInSeconds = Mathf.FloorToInt(_time % 60);
        float timeInMiliseconds = (_time % 1) * 1000;

        string finalTime = string.Format("{0:00}:{1:00}:{2:000}", timeInMinutes, timeInSeconds, timeInMiliseconds);
        string scoreMultiplier;

        if(_score == 0 || _gifts == 0)
        {
            scoreMultiplier = "10";
        }
        else
        {
            scoreMultiplier = (_score / _gifts).ToString();
        }

        
        _giftScore.text = $"Prezenty: {_gifts} x {scoreMultiplier} = {_score}";
        _finalTime.text = $"Czas przejazdu: {finalTime}";
        _victoryScreen.SetActive(true);
        StartCoroutine(EndScreenTimer());
        

        Debug.Log($"Your score: {_score} gifts X 10 = {_score}, Time: {finalTime}. Final score: {_finalScore}");
        
    }

    private void CountTime()
    {
        _time += Time.deltaTime;
        Debug.Log($"Time: {_time}");
    }

    private void CalculateDistance()
    {
        _progressBar.maxValue = Vector3.Distance(_startingLine.transform.position, _finishLine.transform.position);
        _distanceToFinish = Vector3.Distance(_finishLine.transform.position, _player.transform.position);
        _progressBar.value =  _distanceToFinish;
        Debug.Log(_progressBar.value);

    }

    private void FinalScore()
    {
        int tempScore = 0;

        if(_time <= _targetTime)
        {
            tempScore += Convert.ToInt32(_targetTime - _time) * 20;
            _goodJobScreen.SetActive(true);
        }
        else
        {
            _badJobScreen.SetActive(true);
            _giftScore.color = Color.white;
            _finalTime.color = Color.white;
            _finalScore.color = Color.white;
        }

        tempScore += _score;

        _finalScore.text = $"Ostateczny wynik: {tempScore}";
        _scoreScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void PauseMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isPaused = true;
        _pauseScreen.SetActive(true);
        
    }

    public void ContinueGame()
    {
        _pauseScreen.SetActive(false);
        _isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator EndScreenTimer()
    {
        yield return new WaitForSeconds(_timeToShowResults);
        FinalScore();
    }

}
