using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : CustomBehavior
{
    public void MoveTo(Vector3 newPos)
    {
        transform.position = newPos;
    }
}
