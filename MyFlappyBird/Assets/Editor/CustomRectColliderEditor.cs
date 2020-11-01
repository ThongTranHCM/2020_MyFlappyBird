using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomRectCollider))]
public class CustomRectColliderEditor : Editor
{
    // Update is called once per frame

    public override void OnInspectorGUI()
    {
        CustomRectCollider customRectCollider = (CustomRectCollider)target;
        base.OnInspectorGUI();
    }
    void OnSceneGUI()
    {
        CustomRectCollider customRectCollider = (CustomRectCollider)target;

        if (customRectCollider == null || customRectCollider.gameObject == null)
            return;

        // grab the center of the parent
        List<Vector2> points = customRectCollider.ShapePointList();
        Handles.color = Color.green;
        for (int i = 0; i < points.Count - 1; i++)
        {
            Handles.DrawLine(points[i], points[i + 1]);
        }
        Handles.DrawLine(points[points.Count - 1], points[0]);


    }
}
