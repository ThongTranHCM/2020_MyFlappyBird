using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Size-base transform similar to UI. However, these are suppose to be for objects that rarely move.
public class CustomTransform : MonoBehaviour
{
    public bool AdaptToParent = false;
    protected bool calculated = false;
    public Vector2 pMin = -Vector2.one, pMax = Vector2.one;
    public Vector2 anchorMinRatio = Vector2.one * 0.5f, anchorMaxRatio = Vector2.one * 0.5f;
    protected Vector2 anchorMaxPoint, anchorMinPoint;

    public Vector2 pointMax { get { if (!calculated) UpdateSelf(); return anchorMaxPoint + pMax; } }
    public Vector2 pointMin { get { if (!calculated) UpdateSelf(); return anchorMinPoint + pMin; } }

    public Vector3 position { get { return GetPosition(); } set { SetPosition(value); } }

    public CustomTransform ScreenTransform { get { return CameraManager.Instance.customTransform; } }

    void Start()
    {
        if (!calculated) UpdateSelf();
    }
    public CustomTransform GetParent()
    {
        Transform parentTf = transform.parent;
        if (parentTf == null) return ScreenTransform;
        return parentTf.GetComponent<CustomTransform>();
    }

    public virtual void UpdateSelf()
    {
        GetAnchorMax();
        GetAnchorMin();
        calculated = true;
        GetPosition();
    }

    public void UpdateAnchorRatio(Vector2 anchorMaxRatio, Vector2 anchorMinRatio)
    {
        this.anchorMaxRatio = anchorMaxRatio;
        this.anchorMinRatio = anchorMinRatio;
    }
    public void UpdateDelta(Vector2 pMin, Vector2 pMax)
    {
        this.pMin = pMin;
        this.pMax = pMax;
    }


    public virtual Vector2 GetAnchorMax()
    {
        anchorMaxPoint = transform.position;
        if (AdaptToParent)
        {
            CustomTransform parentTf = GetParent();
            if (parentTf == null)
                return anchorMaxPoint;
            Vector2 parentMax = parentTf.pointMax;
            Vector2 parentMin = parentTf.pointMin;
            anchorMaxPoint = new Vector2(Mathf.Lerp(parentMin.x, parentMax.x, anchorMaxRatio.x), Mathf.Lerp(parentMin.y, parentMax.y, anchorMaxRatio.y));
        }
        return anchorMaxPoint;
    }

    public virtual Vector2 GetAnchorMin()
    {
        anchorMinPoint = transform.position;
        if (AdaptToParent)
        {
            CustomTransform parentTf = GetParent();
            if (parentTf == null)
                return anchorMinPoint;
            Vector2 parentMax = parentTf.pointMax;
            Vector2 parentMin = parentTf.pointMin;
            anchorMinPoint = new Vector2(Mathf.Lerp(parentMin.x, parentMax.x, anchorMinRatio.x), Mathf.Lerp(parentMin.y, parentMax.y, anchorMinRatio.y));
        }
        return anchorMinPoint;
    }

    private Vector3 GetPosition()
    {
        if (!calculated) UpdateSelf();
        if (AdaptToParent)
            transform.position = (anchorMaxPoint + pMax + anchorMinPoint + pMin) / 2;
        return transform.position;
    }
    private void SetPosition(Vector3 newPos)
    {
        if (!calculated) UpdateSelf();
        if (AdaptToParent)
        {
            pMax += (Vector2)(newPos - transform.position);
            pMin += (Vector2)(newPos - transform.position);
            transform.position = (anchorMaxPoint + pMax + anchorMinPoint + pMin) / 2;
        }
        CustomCollider customCollider = GetComponent<CustomCollider>();
        if (customCollider != null) customCollider.OnMoving();
    }
}
