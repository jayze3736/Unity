using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemManager))]
public class ItemManagerCustomEditor : Editor
{
    SerializedProperty m_metaDataList;
    SerializedProperty m_folderName;
    SerializedProperty m_fileName;

    SerializationManager manager;

    List<ItemMetaData> list;

    private void OnEnable()
    {
        m_metaDataList = serializedObject.FindProperty("metaDataList");
        m_folderName = serializedObject.FindProperty("folderName");
        m_fileName = serializedObject.FindProperty("fileName");

        manager = new SerializationManager();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        //string folderName = this.m_folderName.stringValue;
        //string fileName = this.m_fileName.stringValue;
        //object list = this.m_metaDataList;


        ////GUILayout.TextField()
        //if (GUILayout.Button("Save"))
        //{
        //    manager.SaveJson(fileName, , folderName);
        //}

        //if (GUILayout.Button("Load"))
        //{

        //    manager.LoadJson<List<ItemMetaData>>(fileName, folderName);
        //    serializedObject.ApplyModifiedProperties();
        //}

        


        

        

    }
    



}
