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
        string firstPropPath = prop.propertyPath; //foreach도 마찬가지이고 child property를 가지고 있는 SerializedProperty는 Enumerator를 먼저 반환하고 그 Enumerator를 iterate 하여 엘리먼트를 가져오거나
        //사용한다. 이때 Enumerator는 prop 내부 필드의 값이므로 복사된 prop 변수의 참조된 실제 원본 프로퍼티의 값을 변경할 수 있고 이로인해서 인덱스가 밀리게된다.
        //따라서 이를 대처하는 방법은 두가지 1. out을 사용하여 직접 참조를 전달하고 처음 propertyPath를 유지할 수 있도록 한다. 2. enumerator를 초기화 
        
        while(enumerator.MoveNext())
        {
            SerializedProperty p = enumerator.Current as SerializedProperty;
           
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)  //SerializedPropertyType.Generic은 프로퍼티 타입중 array, list, struct or class를 나타낸다.
            {
                EditorGUILayout.BeginHorizontal();
                
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName); //fold label 생성
                
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
                
                if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) { continue; }  //동일한 오브젝트를 다시 작성함을 방지하기위해 제어문을 조건을 건 것같다.
                
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

                //동일한 keyword가 미리 그려져있을때 category child로 추가해야함




                foreach(string keyword in keywords)
                {
                    EditorGUILayout.BeginHorizontal();
                    p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, keyword); //fold label 생성
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




        //1. category를 "." 또는 "/"로 구분하여 main category와 sub category들을 분리한다.
        //2. main category를 열면 sub category list들이 그려지며 sub category로 이동하며 이동한 끝의 category 위치에 property를 그린다.
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
