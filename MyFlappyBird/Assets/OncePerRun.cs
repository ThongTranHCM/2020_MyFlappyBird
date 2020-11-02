using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OncePerRun : MonoBehaviour
{
    static bool hasRun = false;
    public UnityEvent firstRun;
    public UnityEvent secondRun;
    // Start is called before the first frame update
    void Start()
    {
        if (hasRun)
        {
            if (secondRun != null) secondRun.Invoke();
        } else
        {
            if (firstRun != null) firstRun.Invoke();
        }
        hasRun = true;
    }
}
