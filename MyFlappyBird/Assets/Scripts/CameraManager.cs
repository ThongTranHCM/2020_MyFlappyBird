using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : CustomBehavior
{
    private static CameraManager _instance;
    public Transform followTarget;
    private Vector3 followDistance;
    public bool fixVertical = true, fixHorizontal = false;
    private Vector2 _size;

    public static CameraManager Instance { get { return _instance; } }
    public Camera InsCamera { get { return GetComponent<Camera>(); } }

    void Awake()
    {
        _instance = this;
        if (followTarget != null)
            followDistance = this.transform.position - followTarget.position;
    }

    void Update()
    {
        if (followTarget != null)
        {
            Vector3 newPos = followTarget.position + followDistance;
            newPos.x = (fixHorizontal) ? transform.position.x : newPos.x;
            newPos.y = (fixVertical) ? transform.position.y : newPos.y;
            transform.position = newPos;

        }
    }

    public Vector2 GetInWorldWidth()
    {
        Vector3 minPos = InsCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 maxPos = InsCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        _size = maxPos - minPos;
        return _size;
    }
}
