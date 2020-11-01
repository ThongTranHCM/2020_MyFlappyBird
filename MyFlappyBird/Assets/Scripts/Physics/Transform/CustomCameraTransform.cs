using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CustomCameraTransform : CustomTransform
{
    public override void UpdateSelf()
    {
        SetFixedValues();
        CalculateSize();
        calculated = true;
    }
    void SetFixedValues()
    {
        anchorMaxRatio = Vector2.one * 0.5f;
        anchorMinRatio = Vector2.one * 0.5f;
        AdaptToParent = false;
    }
    void CalculateSize()
    {
        Camera cam = GetComponent<Camera>();
        Vector3 minPos = cam.ScreenToWorldPoint(Vector3.zero);
        Vector3 maxPos = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        pMin = minPos - transform.position;
        pMax = maxPos - transform.position;
    }

    public override Vector2 GetAnchorMax()
    {
        return transform.position;
    }

    public override Vector2 GetAnchorMin()
    {
        return transform.position;
    }
}
