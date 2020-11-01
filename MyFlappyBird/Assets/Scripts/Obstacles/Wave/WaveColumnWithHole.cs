using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveColumnWithHole : Wave
{
    public int numberOfObstacles;
    public Sprite topSprite, downSprite;
    public GameObject star;
    private int numOfTop, numOfBottom;
    protected override void FirstGenerate()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject obsObject = Instantiate(prefabs[0], transform);
            Obstacle obstacle = obsObject.GetComponent(typeof(Obstacle)) as Obstacle;
            obstacles.Add(obstacle);
        }
        star = Instantiate<GameObject>(Resources.Load<GameObject>("Star"), transform);
        ResetWave();
    }

    public override void ResetWave()
    {
        Vector2 pointMax = customTransform.pointMax;
        Vector2 pointMin = customTransform.pointMin;
        numOfBottom = Random.Range(0, numberOfObstacles);
        numOfTop = 1 - numOfBottom;
        int index;
        float deltaY1, deltaY2;
        for (index = 0, deltaY1 = 0; index < numOfBottom; index++)
        {
            CustomTransform obsTransform = obstacles[index].customTransform;
            obsTransform.UpdateAnchorRatio(new Vector2(0.5f, 0), new Vector2(0.5f, 0));
            obsTransform.UpdateDelta(Vector3.up * deltaY1, Vector3.up * deltaY1);
            obsTransform.UpdateSelf();
            obstacles[index].GetComponent<SpriteRenderer>().sprite = downSprite;
            deltaY1 += obstacles[index].GetComponent<SpriteRenderer>().bounds.size.y;
        }

        for (deltaY2 = 0; index < numberOfObstacles; index++)
        {
            CustomTransform obsTransform = obstacles[index].customTransform;
            obsTransform.UpdateAnchorRatio(new Vector2(0.5f, 1), new Vector2(0.5f, 1));
            obsTransform.UpdateDelta(Vector3.down * deltaY2, Vector3.down * deltaY2);
            obsTransform.UpdateSelf();
            obstacles[index].GetComponent<SpriteRenderer>().sprite = topSprite;
            deltaY2 += obstacles[index].GetComponent<SpriteRenderer>().bounds.size.y;
        }
        star.gameObject.SetActive(true);
        star.transform.localPosition = Vector2.up * (deltaY1 - deltaY2) / 2;
    }
}
