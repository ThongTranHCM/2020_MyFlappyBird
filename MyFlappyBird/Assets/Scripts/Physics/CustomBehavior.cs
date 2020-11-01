using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehavior : MonoBehaviour
{
    private CustomTransform _customTransform;
    public CustomTransform customTransform
    {
        get
        {
            if (_customTransform == null)
                _customTransform = GetComponent<CustomTransform>();
            return _customTransform;
        }
    }
    private CustomCollider _customCollider;
    public CustomCollider customCollider
    {
        get
        {
            if (_customCollider == null)
                _customCollider = GetComponent<CustomCollider>();
            return _customCollider;
        }
    }

    public virtual void OnCollide(CustomCollider collider)
    {
    }
}
