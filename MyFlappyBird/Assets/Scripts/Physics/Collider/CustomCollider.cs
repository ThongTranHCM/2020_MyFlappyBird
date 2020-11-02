using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomCollider : MonoBehaviour
{
    public bool CheckCollide = false;
    public Vector2 offSet, size;

    public void Start()
    {
        CustomColliderManager.Instance.AddCollider(this);
    }

    public void OnDestroy()
    {
        CustomColliderManager.Instance.RemoveCollider(this);
    }

    public void OnMoving()
    {
        CustomColliderManager.Instance.OnColliderMove(this);
    }

    public void OnCollide(CustomCollider otherCollider)
    {
        CustomBehavior customBehavior = GetComponent<CustomBehavior>();
        if (customBehavior != null)
            customBehavior.OnCollide(otherCollider);
    }

    public virtual void MatchSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        Sprite sprite = spriteRenderer.sprite;
        if (sprite == null) return;
        size = spriteRenderer.bounds.size / 2;
        Vector2 sprOffset = (sprite.rect.center - sprite.pivot);
        Vector2 half = (sprite.rect.max - sprite.rect.center);
        offSet = new Vector2((sprOffset.x / half.x) * size.x, (sprOffset.y / half.y) * size.y);
    }

    public abstract void BorderPoint(out Vector2 min, out Vector2 max);

    public bool DoesBordersCollide(CustomCollider otherCollider)
    {
        Vector2 min1, max1, min2, max2;
        this.BorderPoint(out min1, out max1);
        otherCollider.BorderPoint(out min2, out max2);
        if (max1.x <= min2.x || max2.x <= min1.x)
            return false;
        if (max1.y <= min2.y || max2.y <= min1.y)
            return false;

        return true;
    }

    public virtual bool IsCollide(CustomCollider otherCollider)
    {
        return DoesBordersCollide(otherCollider);
    }

    public virtual List<Vector2> ShapePointList()
    {
        Vector2 min, max;
        this.BorderPoint(out min, out max);
        List<Vector2> points = new List<Vector2>();
        points.Add(min);
        points.Add(new Vector2(max.x, min.y));
        points.Add(max);
        points.Add(new Vector2(min.x, max.y));
        return points;
    }
}
