using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemMetaDataSetEditorWindow : ExtendedEditorWindow
{

    


    public static void Open(ItemMetaDataSet container)
    {

        ItemMetaDataSetEditorWindow window = GetWindow<ItemMetaDataSetEditorWindow>("Item meta data editor");
        window.serializedObject = new SerializedObject(container);

    }

    public void OnGUI()
    {
        serializedObject.Update();
        currentProperty = serializedObject.FindProperty("dataSet");
        

        EditorGUILayout.BeginHorizontal();


            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
                DrawSidebar(currentProperty);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
                if(selectedProperty != null)
                {
                    DrawProperties(selectedProperty, true);
                }
                else
                {
                    EditorGUILayout.LabelField("select an item from the list");
                }
            EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        Apply();



    }



}
