using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : CustomBehavior
{
    public float parallaxEffect;
    [Range(1, 2)]
    public int dimension = 1;
    private float minX, maxX, spriteLength;
    public GameObject backgroundPiece;
    private Vector3 startingPos;

    public void Start()
    {
        startingPos = customTransform.position;
        spriteLength = backgroundPiece.GetComponent<SpriteRenderer>().bounds.size.x;
        GetBorder();
        InstantiatePool();
    }

    public void GetBorder()
    {
        Sprite renderSprite = backgroundPiece.GetComponent<SpriteRenderer>().sprite;
        minX = -spriteLength * 2 + customTransform.pointMin.x;
        maxX = spriteLength * 2 + customTransform.pointMax.x;
    }
    public void LateUpdate()
    {
        float dist = Camera.main.transform.position.x * parallaxEffect;
        float temp = Camera.main.transform.position.x * (1 - parallaxEffect);
        while (temp < startingPos.x - spriteLength)
            startingPos += Vector3.left * spriteLength;
        while (temp > startingPos.x + spriteLength)
            startingPos += Vector3.right * spriteLength;
        transform.position = startingPos + Vector3.right * dist;
    }

    public void InstantiatePool()
    {
        Vector3 spawn = Vector3.zero;
        spawn.x = minX;
        while (spawn.x < maxX)
        {
            GameObject childObject = Instantiate(backgroundPiece, transform);
            childObject.transform.localPosition = spawn;

            spawn += Vector3.right * spriteLength;
        };
    }
}
