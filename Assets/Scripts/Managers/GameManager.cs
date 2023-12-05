using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Private variables
    [SerializeField] private int _score;
    [SerializeField] private int _scoreDeduction;
    [SerializeField] private TextMeshProUGUI _scoreText;

    //Public variables
    public int Score { get { return _score; }}
    public int ScoreDeduction {  get { return _scoreDeduction; }}
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
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"Score: {_score}";
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

}
