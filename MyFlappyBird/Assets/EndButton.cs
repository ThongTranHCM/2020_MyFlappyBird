using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
