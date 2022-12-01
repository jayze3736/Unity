using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;


    protected string selectedPropertyPath;
    protected SerializedProperty selectedProperty;


 

    




    protected SerializedProperty DrawProperties(SerializedProperty prop, bool drawChildren)
    {
        
        if (prop == null) return null;
        
        string lastPropPath = string.Empty;
        IEnumerator enumerator = prop.GetEnumerator();
        string firstPropPath = prop.propertyPath; //foreach�� ���������̰� child property�� ������ �ִ� SerializedProperty�� Enumerator�� ���� ��ȯ�ϰ� �� Enumerator�� iterate �Ͽ� ������Ʈ�� �������ų�
        //����Ѵ�. �̶� Enumerator�� prop ���� �ʵ��� ���̹Ƿ� ����� prop ������ ������ ���� ���� ������Ƽ�� ���� ������ �� �ְ� �̷����ؼ� �ε����� �и��Եȴ�.
        //���� �̸� ��ó�ϴ� ����� �ΰ��� 1. out�� ����Ͽ� ���� ������ �����ϰ� ó�� propertyPath�� ������ �� �ֵ��� �Ѵ�. 2. enumerator�� �ʱ�ȭ 
        
        while(enumerator.MoveNext())
        {
            SerializedProperty p = enumerator.Current as SerializedProperty;
           
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)  //SerializedPropertyType.Generic�� ������Ƽ Ÿ���� array, list, struct or class�� ��Ÿ����.
            {
                EditorGUILayout.BeginHorizontal();
                
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName); //fold label ����
                
                EditorGUILayout.EndHorizontal();

                
                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;
                }
                

            }
            else
            {
                
                if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) { continue; }  //������ ������Ʈ�� �ٽ� �ۼ����� �����ϱ����� ����� ������ �� �Ͱ���.
                
                lastPropPath = p.propertyPath;
                
                EditorGUILayout.PropertyField(p, drawChildren);
                

            }
            

        }
       
        prop = serializedObject.FindProperty(firstPropPath);
        return prop;




    }

    protected void DrawSidebar(SerializedProperty prop)
    {
        foreach(SerializedProperty p in prop)
        {
            if (GUILayout.Button(p.displayName))
            {
                selectedPropertyPath = p.propertyPath;
            }

            if (!string.IsNullOrEmpty(selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
            }



        }




    }


    

    protected void DrawCategoryBar(SerializedProperty prop, string category, char splitchar)
    {


        if(prop.isArray && prop.propertyType == SerializedPropertyType.Generic)
        {
            foreach(SerializedProperty p in prop)
            {
                SerializedProperty property = p.FindPropertyRelative("category");
                string categoryName = property.stringValue;
                string[] keywords = categoryName.Split(splitchar);

                //������ keyword�� �̸� �׷��������� category child�� �߰��ؾ���




                foreach(string keyword in keywords)
                {
                    EditorGUILayout.BeginHorizontal();
                    p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, keyword); //fold label ����
                    EditorGUILayout.EndHorizontal();
                }
                

                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, true);
                    EditorGUI.indentLevel--;
                }



            }
        }

        
        GUILayout.BeginVertical();




        //1. category�� "." �Ǵ� "/"�� �����Ͽ� main category�� sub category���� �и��Ѵ�.
        //2. main category�� ���� sub category list���� �׷����� sub category�� �̵��ϸ� �̵��� ���� category ��ġ�� property�� �׸���.
        //3. 

    }



    public void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
