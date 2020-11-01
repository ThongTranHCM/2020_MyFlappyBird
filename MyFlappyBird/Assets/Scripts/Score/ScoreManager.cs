using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }
    int score = 0;

    public ScoreUI scoreUI;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        scoreUI.UpdateScore(score);
    }
    public void AddScore(int incScore)
    {
        score += incScore;
        scoreUI.UpdateScore(score);
    }
}
