using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;



//직렬화된 프로퍼티에 대한 메타 정보를 저장하는 클래스
public class SerializedPropertyMetaData
{
    string name; 
    SerializedPropertyType propertyType;
    Type objectRefType;
    object value;
    string propertyPath;
   

    public string Name { get => name;  }
    public object Value { get => value;  set { this.value = value; } }
    public string PropertyPath { get => propertyPath;}
    public SerializedPropertyType PropertyType { get => propertyType; }
    public Type RefType { get => objectRefType; }

    public SerializedPropertyMetaData(string name, string propertyPath, SerializedPropertyType propertyType, Type refType)
    {
        this.name = name;
        this.propertyPath = propertyPath;
        this.propertyType = propertyType;
        this.objectRefType = refType;
    }
    public SerializedPropertyMetaData(string name, string propertyPath, SerializedPropertyType propertyType)
    {
        this.name = name;
        this.propertyPath = propertyPath;
        this.propertyType = propertyType;
        
    }

    public SerializedPropertyMetaData()
    {

    }


}



/// <summary>
/// Array 타입의 Serialized Property를 가지고 있는 직렬화 오브젝트에 새로운 엘리먼트를 추가하는 기능을 제공하는 창
/// </summary>
public class AddNewPropertyElementWindow : ExtendedEditorWindow
{
    SerializedProperty arraylist; //Arry 타입의 Serialized Property
    int lastindex; //arrylist의 끝 인덱스
    List<SerializedPropertyMetaData> props; //새로 생성할 직렬화된 오브젝트의 Serialized Property의 메타정보를 보관하는 리스트
    
    
    bool readPropMeta = false; //property의 meta 정보를 읽었는지 표시한다.
    
    

    public static EditorWindow Open(SerializedProperty arraylist, SerializedObject serializedObject)
    {
        //스태틱 메소드 안에서는 원칙상, 스태틱 데이터만 사용이 가능하나, 이 메소드 안에서 동적생성한 지역변수는 이 변수 내에서만 접근이 가능하다.
        AddNewPropertyElementWindow window = GetWindow<AddNewPropertyElementWindow>("Property Editor");

        if (!arraylist.isArray)
        {
            Debug.LogError("arraylist is not array");
            return null;
        }

        window.arraylist = arraylist;
        window.serializedObject = serializedObject; //동일한 직렬화 오브젝트를 참조

        window.lastindex = arraylist.arraySize - 1;
        if(window.lastindex == -1)
        {
            window.lastindex = 0;
        }

        return window;
       

    }


   

    public void DrawFieldLayout(List<SerializedPropertyMetaData> proplist)
    {

        foreach(SerializedPropertyMetaData p in proplist)
        {

            if (p.PropertyType == SerializedPropertyType.String)
            {

                EditorGUILayout.PrefixLabel(p.Name);
                p.Value = EditorGUILayout.TextField((string)p.Value);
               

            }
            else if (p.PropertyType == SerializedPropertyType.ObjectReference)
            { 
                p.Value = EditorGUILayout.ObjectField(p.Name, (UnityEngine.Object)p.Value, p.RefType, true);

            }



        }



    }


    public void ReadPropMeta(SerializedProperty prop)
    {
        foreach (SerializedProperty p in prop)
        {

            
            SerializedPropertyMetaData data;
            if(p.propertyType == SerializedPropertyType.ObjectReference) //primitive 타입이 아닌 reference type일 경우 
            {
                
                
                Type type = GetTypeFromSerializedProperty(p); //property에 해당하는 Type 값을 가져온다.
                data = new SerializedPropertyMetaData(p.name, p.propertyPath, p.propertyType, type);
                
                
            }
            else
            {
                data = new SerializedPropertyMetaData(p.name, p.propertyPath, p.propertyType);
            }
            props.Add(data);
        }
    }

    /// <summary>
    /// 리플렉션을 사용하여 주어진 Type의 필드내에 array가 없을때까지 찾고자하는 Property가 있는지 조사하고 만약 있으면 그 Property의 타입과 동일한 필드 타입(Type)을 반환한다.
    /// </summary>
    /// <param name="targetType"></param>
    /// <param name="targetProp"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    public Type TryGetArrayElementFieldType(Type targetType, SerializedProperty targetProp, BindingFlags flags)
    {
        string name = targetProp.name; //필드 이름
        string typeName = GetSerializedPropTypeName(targetProp); //변수 이름이 동일하지만 찾고자하는 타입이 다른 변수를 배제하기위한 타입 이름
        

        if (string.IsNullOrEmpty(typeName))
        {
            Debug.Log("typeName is null or Empty");
            return null;
            
        }

        
        FieldInfo field = targetType.GetField(name, flags);
       

        Type elementType = null;

        if (field == null)
        {
            
            FieldInfo[] fields = targetType.GetFields(flags);
            
            foreach (FieldInfo f in fields)
            {
                

                //또 array가 존재할 경우
                if (typeof(IEnumerable).IsAssignableFrom(f.FieldType)) //f.FieldType이 IEnumerable인지 확인 = IEnumerable이라는 타입변수에 이 타입을 대입할 수 있는지 물어봄
                {
                    
                    
                    
                    Type nextType = f.FieldType.GetGenericArguments()[0];
                    
                    FieldInfo[] nextFields = nextType.GetFields(flags);
                    elementType = TryGetArrayElementFieldType(nextType, targetProp, flags);
                    
                    //재귀 
                    //array를 대상으로 해당 prop name이 존재하는지 확인
                }
            }
        }
        else
        {
           
            string fieldName = field.FieldType.ToString(); //field.GetType().ToString()이랑 다른 결과를 냄
            string [] names = fieldName.Split('.');
            int index = (names.Length - 1 == -1) ? 0 : (names.Length - 1); 
            string fieldTypeName = names[index]; //마지막에 위치한 단어가 필드 타입의 이름과 동일함
            
            if (typeName != fieldTypeName) //Property에서의 Type과 리플렉션을 통한 동일한 이름의 필드 타입이 같은지 확인후 다를 경우 다시 조사
            {
                FieldInfo[] fields = targetType.GetFields(flags);
                foreach (FieldInfo f in fields)
                {
                    if (typeof(IEnumerable).IsAssignableFrom(f.FieldType))
                    {
                        //field = f;
                        Type nextType = f.FieldType.GetGenericArguments()[0];
                        FieldInfo[] nextFields = nextType.GetFields(flags);
                        elementType = TryGetArrayElementFieldType(nextType, targetProp, flags);
                        
                        //재귀 
                        //array를 대상으로 해당 prop name이 존재하는지 확인
                    }
                }
            }
            else
            {
                return field.FieldType;
            }
        }

        return elementType;
        
    }

    public string GetSerializedPropTypeName(SerializedProperty p)
    {
        var match = Regex.Match(p.type, @"PPtr<\$(.*?)>");
        string capture = string.Empty;
        if (match.Success)
            capture = match.Groups[1].Value;
        return capture;
    }


    public Type GetTypeFromSerializedProperty(SerializedProperty prop)
    {
        
        Type type = prop.serializedObject.targetObject.GetType();
        Type resultType = TryGetArrayElementFieldType(type, prop, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        return resultType;

    }


    

    

    private void OnGUI()
    {
        
       
        if (!arraylist.isArray)
        {
            EditorGUILayout.LabelField("There is no array field in ScriptableObject, your ScriptableObject should have List field");
            Debug.LogError("arraylist is not array");
            return;
        }



        if (!readPropMeta)
        {
            props = new List<SerializedPropertyMetaData>();
            arraylist.InsertArrayElementAtIndex(lastindex); //임시로 property 먼저 추가
            SerializedProperty tempProp = arraylist.GetArrayElementAtIndex(lastindex); //생성한 프로퍼티 데이터 가져옴
                ReadPropMeta(tempProp); //props는 이제 다음에 추가될 프로퍼티(예정)의 프로퍼티 경로를 미리 알고있고 현재 추가될 property의 타입, 이름등을 저장한다.
            arraylist.DeleteArrayElementAtIndex(lastindex);
            readPropMeta = true; 
        }

        //데이터는 제출을 하기전까진 저장을 한번만 해야하고 제출한다음엔 초기화를 해서 새로운 값을 입력받아서 Element를 추가할 수 있어야됨

        //데이터 필드를 생성
        DrawFieldLayout(props);
        


        if (GUILayout.Button("Add"))
        {
            arraylist.InsertArrayElementAtIndex(lastindex); //property 먼저 추가
           
           

           foreach(SerializedPropertyMetaData p in props)
            {
                
                SerializedProperty newChildProp = serializedObject.FindProperty(p.PropertyPath);
                
                if (newChildProp == null)
                {
                    Debug.LogError("Couldn't find property at path:" + p.PropertyPath);
                    continue;
                }

                if (newChildProp.propertyType == SerializedPropertyType.String)
                {
                    newChildProp.stringValue = (string)p.Value;
                }
                else if(newChildProp.propertyType == SerializedPropertyType.ObjectReference)
                {
                    newChildProp.objectReferenceValue = (UnityEngine.Object)p.Value;
                }
            }
                
                
            
            Apply();
            EditorWindow updateWindow = GetWindow<ItemMetaDataSetCategoryEditorWindow>();
            updateWindow.SendEvent(EditorGUIUtility.CommandEvent("UpdateCategoryTree")); //새로운 요소가 추가가되면 이 창 아래에 열려있는 parentWindow의 OnGUI 진행을 초기화하고 다시 실행되도록 해야함
           


            readPropMeta = false; 
            lastindex++;
            return;
            
        }
        if (GUILayout.Button("Cancel"))
        {
            readPropMeta = false;
            
        }

        




    }

    //Window가 닫힐때
    private void OnDestroy()
    {
        
        //

    }




}
