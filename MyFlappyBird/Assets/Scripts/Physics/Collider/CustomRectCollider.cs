using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRectCollider : CustomCollider
{
    public override void BorderPoint(out Vector2 min, out Vector2 max)
    {
        List<Vector2> points = ShapePointList();
        min = points[0];
        max = points[0];
        foreach (Vector3 point in points)
        {
            if (point.x < min.x) min.x = point.x;
            if (point.x > max.x) max.x = point.x;
            if (point.y < min.y) min.y = point.y;
            if (point.y > max.y) max.y = point.y;
        }
    }

    /*public override bool IsCollide(CustomCollider otherCollider)
    {
        if (otherCollider.GetType() == typeof(CustomRectCollider))
            return IsCollideWithCustomRectCollider((CustomRectCollider)otherCollider);
        return false;
    }*/

    public bool IsCollideWithCustomRectCollider(CustomRectCollider otherCollider)
    {
        List<Vector2> rect1 = this.ShapePointList();
        List<Vector2> rect2 = otherCollider.ShapePointList();
        foreach (Vector2 point in rect1)
        {
            Ray ray = new Ray(rect2[0], rect2[0] - rect2[1]);
            float distance = Vector3.Cross(rect2[1] - rect2[0], point - rect2[0]).magnitude;
        }

        return false;

    }

    public override List<Vector2> ShapePointList()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(transform.TransformPoint(offSet + new Vector2(-size.x, -size.y)));
        points.Add(transform.TransformPoint(offSet + new Vector2(size.x, -size.y)));
        points.Add(transform.TransformPoint(offSet + new Vector2(size.x, size.y)));
        points.Add(transform.TransformPoint(offSet + new Vector2(-size.x, size.y)));
        return points;
    }
}
