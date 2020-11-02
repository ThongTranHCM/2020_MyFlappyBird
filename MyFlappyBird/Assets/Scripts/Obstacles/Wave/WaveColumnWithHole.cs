using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveColumnWithHole : Wave
{
    public int numberOfObstacles;
    private GameObject star;
    private int numOfTop, numOfBottom;

    private Obstacle ObstacleTop { get { return obstacles[0]; } }
    private Obstacle ObstacleBottom { get { return obstacles[1]; } }
    protected override void FirstGenerate()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject obsObject = Instantiate(prefabs[i], transform);
            Obstacle obstacle = obsObject.GetComponent(typeof(Obstacle)) as Obstacle;
            obstacles.Add(obstacle);
        }
        ObstacleTop.customTransform.UpdateAnchorRatio(new Vector2(0.5f, 0), new Vector2(0.5f, 0));
        ObstacleBottom.customTransform.UpdateAnchorRatio(new Vector2(0.5f, 1), new Vector2(0.5f, 1));
        star = Instantiate<GameObject>(Resources.Load<GameObject>("Star"), transform);
        ResetWave();
    }

    public override void ResetWave()
    {
        numOfBottom = Random.Range(0, numberOfObstacles);
        numOfTop = numberOfObstacles - numOfBottom;
        ObstacleBottom.Scale(1, numOfBottom);
        ObstacleTop.Scale(1, numOfTop);
        star.gameObject.SetActive(true);
        star.transform.localPosition = Vector2.down * (numOfBottom - numOfTop) * 0.24f;
    }
}
