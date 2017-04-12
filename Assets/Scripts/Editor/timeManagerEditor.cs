using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(timeManager))]
public class timeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        timeManager myScript = (timeManager)target;
        if (GUILayout.Button("Play"))
        {
            myScript.Play();
        }

        if (GUILayout.Button("Pause"))
        {
            myScript.Pause();
        }

        if (GUILayout.Button("Rewind"))
        {
            myScript.Rewind();
        }

        if (GUILayout.Button("FF"))
        {
            myScript.FastForward();
        }


    }
}
