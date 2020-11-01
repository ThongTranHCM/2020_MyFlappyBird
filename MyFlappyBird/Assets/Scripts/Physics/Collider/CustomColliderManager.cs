using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColliderManager : MonoBehaviour
{
    static CustomColliderManager _instance;
    public static CustomColliderManager Instance { get { return _instance; } }
    List<CustomCollider> colliders = new List<CustomCollider>();
    List<CustomCollider> movedColliders = new List<CustomCollider>();

    public void Awake()
    {
        _instance = this;
    }
    public void AddCollider(CustomCollider collider)
    {
        if (collider != null)
            colliders.Add(collider);
    }

    public void RemoveCollider(CustomCollider collider)
    {
        colliders.Remove(collider);
    }

    public void OnColliderMove(CustomCollider targetCollider)
    {
        if (!targetCollider.CheckCollide)
            return;
        foreach (CustomCollider collider in colliders)
        {
            if (collider.enabled && collider.gameObject.activeInHierarchy)
                if (collider != targetCollider && targetCollider.IsCollide(collider)) {
                    targetCollider.OnCollide(collider);
                    collider.OnCollide(targetCollider);
                }
        }
    }
}
