using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StreetManager))]
public class StreetManagerEditor : Editor
{
    private StreetManager script;
    private void OnEnable()
    {
        script = (StreetManager)target;
    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add Vertical UP"))
        {

            script.AddVerticalStreet(POS.UP);

        }

        if (GUILayout.Button("Add Intersection UP"))
        {

            script.AddIntersectionStreet(POS.UP);

        }
        if (GUILayout.Button("Add Intersection LEFT"))
        {
            script.AddIntersectionStreet(POS.LEFT);
        }
        if (GUILayout.Button("Add Intersection RIGHT"))
        {
            script.AddIntersectionStreet(POS.RIGHT);
        }

        if (GUILayout.Button("Add Horizontal LEFT"))
        {

            script.AddHorizontalStreet(POS.LEFT);

        }
        if (GUILayout.Button("Add Horizontal RIGHT"))
        {

            script.AddHorizontalStreet(POS.RIGHT);

        }

        
        base.OnInspectorGUI();
    }
   
}
