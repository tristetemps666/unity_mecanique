// using System.Collections;
// using JetBrains.Annotations;
// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(Attack)), CanEditMultipleObjects]
// public class CI_IAttack : Editor
// {
//     bool DebugBouton = false;

//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         DebugBouton = EditorGUILayout.Toggle("Activer Boutons de debug", DebugBouton);

//         Attack myScript = (Attack)target;
//         if (DebugBouton)
//         {
//             if (GUILayout.Button("Start l'attaque"))
//             {
//                 myScript.DoAttack();
//             }
//         }
//     }
// }
