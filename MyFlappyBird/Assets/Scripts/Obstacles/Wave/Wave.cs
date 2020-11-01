using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : CustomBehavior
{
    public List<GameObject> prefabs;
    protected List<Obstacle> obstacles = new List<Obstacle>();
    protected List<ScoreStar> scoreStar = new List<ScoreStar>();

    public Vector3 position
    {
        get { return transform.position; }
        set
        {
            customTransform.position = value;
        }
    }

    protected abstract void FirstGenerate();
    public abstract void ResetWave();

    // Start is called before the first frame update
    void Start()
    {
        FirstGenerate();
    }
}
