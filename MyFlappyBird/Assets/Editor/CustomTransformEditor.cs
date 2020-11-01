using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomTransform))]
public class CustomTransformEditor : Editor
{

    public override void OnInspectorGUI()
    {
        CustomTransform customTransform = (CustomTransform)target;
        base.OnInspectorGUI();
    }
}