using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CustomItemMetaDataSet))]
public class ItemMetaDataSetCategoryEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Open Editor"))
        {
            ItemMetaDataSetCategoryEditorWindow.Open((CustomItemMetaDataSet)target);
        }


    }

}
