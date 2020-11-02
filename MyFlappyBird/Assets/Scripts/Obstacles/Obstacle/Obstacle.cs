using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : CustomBehavior
{
    public void MoveTo(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void Scale(int numOfHorizontalTile, int numOfVerticalTile)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.size = new Vector2(spriteRenderer.sprite.bounds.size.x * numOfHorizontalTile, spriteRenderer.sprite.bounds.size.y * numOfVerticalTile);
            customCollider.MatchSprite();
        }
    }
}
