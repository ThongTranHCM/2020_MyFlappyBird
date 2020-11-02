using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public enum ScoreValue {
        CurrentScore,
        BestScore
    }
    public List<Image> digits;
    public ScoreValue scoreValue;
    public static Sprite[] digitSprites;

    void Start()
    {
        switch (scoreValue)
        {
            case ScoreValue.CurrentScore:
                ScoreManager.Instance.RegisterCurrentScoreUI(this);
                break;
            case ScoreValue.BestScore:
                ScoreManager.Instance.RegisterBestScoreUI(this);
                break;
        }
    }

    static Sprite DigitSprite(int digit)
    {
        if (digitSprites == null)
            digitSprites = Resources.LoadAll<Sprite>("scoreDigit");
        if (digit < 0 || digit > 9) return null;
        return digitSprites[digit];
    }

    public void UpdateScore(int newScore)
    {
        int index = 0;
        if (newScore == 0)
        {
            digits[0].sprite = DigitSprite(0);
            index = 1;
        }
        while (newScore > 0)
        {
            digits[index].enabled = true;
            digits[index].sprite = DigitSprite(newScore % 10);
            newScore /= 10;
            index++;
        }
        for (;index < digits.Count; index++)
        {
            digits[index].enabled = false;
        }
    }
}
