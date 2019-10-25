using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TEst))]
public class WfcInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TEst myScript = (TEst)target;
        if(GUILayout.Button("Create tilemap"))
        {
            myScript.CreateWFC();
            myScript.CreateTilemap();
        }
        if(GUILayout.Button("Save tilmep"))
        {
            myScript.SaveTilemap();
        }
    }
}
