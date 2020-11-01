using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStar : CustomBehavior
{
    public void Reset()
    {
        gameObject.SetActive(true);
    }
    public override void OnCollide(CustomCollider collider)
    {
        if (collider.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(1);
            gameObject.SetActive(false);
        }
    }
}
