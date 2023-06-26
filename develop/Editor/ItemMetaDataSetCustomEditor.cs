using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


public class AssetHandler
{


    [OnOpenAsset()]
    public static bool OnOpenEditor(int instanceId, int line)
    {

        ItemMetaDataSet set = EditorUtility.InstanceIDToObject(instanceId) as ItemMetaDataSet;
        if(set != null)
        {
            ItemMetaDataSetEditorWindow.Open(set);
            return true;
        }
        return false;
    }




}




[CustomEditor(typeof(ItemMetaDataSet))]
public class ItemMetaDataSetCustomEditor : Editor
{

   

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Open Editor"))
        {
            ItemMetaDataSetEditorWindow.Open((ItemMetaDataSet)target);




        }


    }




}
