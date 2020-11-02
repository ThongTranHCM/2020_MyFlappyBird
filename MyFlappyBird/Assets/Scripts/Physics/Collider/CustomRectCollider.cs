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

    public override bool IsCollide(CustomCollider otherCollider)
    {
        if (otherCollider.GetType() == typeof(CustomRectCollider))
            return IsCollideWithCustomRectCollider((CustomRectCollider)otherCollider);
        return false;
    }

    public bool IsCollideWithCustomRectCollider(CustomRectCollider otherCollider)
    {
        List<Vector2> rect1 = this.ShapePointList();
        foreach (Vector2 point in rect1)
        {
            if (otherCollider.IsContain(point)) return true;
        }
        List<Vector2> rect2 = otherCollider.ShapePointList();
        foreach (Vector2 point in rect2)
        {
            if (IsContain(point)) return true;
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

    public override bool IsContain(Vector2 point)
    {
        List<Vector2> rect = this.ShapePointList();
        Vector2 vAM = point - rect[1];
        Vector2 vAB = rect[0] - rect[1];
        float dotAMAB = Vector2.Dot(vAM, vAB);
        if (0 >= dotAMAB || dotAMAB >= vAB.magnitude * vAB.magnitude)
            return false;
        Vector2 vAD = rect[2] - rect[1];
        float dotAMAD = Vector2.Dot(vAM, vAD);
        if (0 >= dotAMAD || dotAMAD >= vAD.magnitude * vAD.magnitude)
            return false;
        return true;
    }
}
