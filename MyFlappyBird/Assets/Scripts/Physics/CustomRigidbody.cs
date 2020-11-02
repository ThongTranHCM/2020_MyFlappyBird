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
        transform.rotation = UpdateRotation(new Vector2(velocity.x, velocity.y / 2));
    }

    public static Quaternion UpdateRotation(Vector2 direction)
    {
        // rotate that vector by 90 degrees around the Z axis
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;

        // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
        // (resulting in the X axis facing the target)
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
        return targetRotation;
    }
}
