using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 accelerate;

    public Vector3 position { get { return transform.position; }
        set
        {
            CustomTransform customTransform = GetComponent<CustomTransform>();
            if (customTransform != null)
                customTransform.position = value;
            else transform.position = value;
        } }

    private void FixedUpdate()
    {
        position += velocity * Time.fixedDeltaTime + accelerate * Time.fixedDeltaTime * Time.fixedDeltaTime / 2;
        velocity += Time.fixedDeltaTime * accelerate;
        //transform.rotation = Quaternion.Euler(0, 0, -30 * _timeSinceTop * Mathf.Abs(_timeSinceTop) - 40 * _timeSinceTop);
    }
}
