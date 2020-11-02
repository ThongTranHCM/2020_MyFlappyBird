using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    private List<ScoreUI> currentScoreUIs = new List<ScoreUI>();
    private List<ScoreUI> bestScoreUIs = new List<ScoreUI>();
    int currentScore = 0;

    public UnityEvent onScoreChange;
    public int CurrentScore { get { return currentScore; }
        set
        {
            currentScore = value;
            foreach (ScoreUI scoreUI in currentScoreUIs)
            {
                scoreUI.UpdateScore(currentScore);
            }
            if (onScoreChange != null)
                onScoreChange.Invoke();
        }
    }

    public static int BestScore { get { return PlayerPrefs.GetInt("High Score", 0); }
        set
        {
            int currentBest = PlayerPrefs.GetInt("High Score", 0);
            if (value > currentBest)
            {
                PlayerPrefs.SetInt("High Score", value);
                if (Instance != null)
                foreach (ScoreUI scoreUI in Instance.bestScoreUIs)
                {
                    scoreUI.UpdateScore(value);
                }
            }
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void RegisterCurrentScoreUI(ScoreUI scoreUI)
    {
        currentScoreUIs.Add(scoreUI);
        scoreUI.UpdateScore(CurrentScore);
    }

    public void RegisterBestScoreUI(ScoreUI scoreUI)
    {
        currentScoreUIs.Add(scoreUI);
        scoreUI.UpdateScore(BestScore);
    }

    public void SubmiteNewScore()
    {
        if (CurrentScore > BestScore)
            BestScore = CurrentScore;
    }
}
