using System.Collections;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletManager))]
public class CI_BulletManager : Editor
{
    bool DebugBouton = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DebugBouton = EditorGUILayout.Toggle("Activer Boutons de debug", DebugBouton);

        BulletManager myScript = (BulletManager)target;
        if (DebugBouton)
        {
            if (GUILayout.Button("Start Spawn Bullets"))
            {
                myScript.StartSpawning();
            }
            if (GUILayout.Button("Stop Spawn Bullets"))
            {
                myScript.StopSpawning();
            }
        }
    }
}
