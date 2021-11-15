using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (NanoBot))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI(){
        NanoBot n = (NanoBot)target;
        Vector2 viewA = n.DirFromAngle(-n.viewAngle/2);
        Vector2 viewB = n.DirFromAngle(n.viewAngle/2);

        Handles.DrawWireArc(n.transform.position, Vector3.forward, Vector2.left, 360, n.lookahead);        
        Handles.DrawLine(n.transform.position, (Vector2)n.transform.position + viewA * n.lookahead);
        Handles.DrawLine(n.transform.position, (Vector2)n.transform.position + viewB * n.lookahead);
    }
}
