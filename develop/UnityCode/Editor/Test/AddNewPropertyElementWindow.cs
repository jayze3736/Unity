using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;



//����ȭ�� ������Ƽ�� ���� ��Ÿ ������ �����ϴ� Ŭ����
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
/// Array Ÿ���� Serialized Property�� ������ �ִ� ����ȭ ������Ʈ�� ���ο� ������Ʈ�� �߰��ϴ� ����� �����ϴ� â
/// </summary>
public class AddNewPropertyElementWindow : ExtendedEditorWindow
{
    SerializedProperty arraylist; //Arry Ÿ���� Serialized Property
    int lastindex; //arrylist�� �� �ε���
    List<SerializedPropertyMetaData> props; //���� ������ ����ȭ�� ������Ʈ�� Serialized Property�� ��Ÿ������ �����ϴ� ����Ʈ
    
    
    bool readPropMeta = false; //property�� meta ������ �о����� ǥ���Ѵ�.
    
    

    public static EditorWindow Open(SerializedProperty arraylist, SerializedObject serializedObject)
    {
        //����ƽ �޼ҵ� �ȿ����� ��Ģ��, ����ƽ �����͸� ����� �����ϳ�, �� �޼ҵ� �ȿ��� ���������� ���������� �� ���� �������� ������ �����ϴ�.
        AddNewPropertyElementWindow window = GetWindow<AddNewPropertyElementWindow>("Property Editor");

        if (!arraylist.isArray)
        {
            Debug.LogError("arraylist is not array");
            return null;
        }

        window.arraylist = arraylist;
        window.serializedObject = serializedObject; //������ ����ȭ ������Ʈ�� ����

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
            if(p.propertyType == SerializedPropertyType.ObjectReference) //primitive Ÿ���� �ƴ� reference type�� ��� 
            {
                
                
                Type type = GetTypeFromSerializedProperty(p); //property�� �ش��ϴ� Type ���� �����´�.
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
    /// ���÷����� ����Ͽ� �־��� Type�� �ʵ峻�� array�� ���������� ã�����ϴ� Property�� �ִ��� �����ϰ� ���� ������ �� Property�� Ÿ�԰� ������ �ʵ� Ÿ��(Type)�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="targetType"></param>
    /// <param name="targetProp"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    public Type TryGetArrayElementFieldType(Type targetType, SerializedProperty targetProp, BindingFlags flags)
    {
        string name = targetProp.name; //�ʵ� �̸�
        string typeName = GetSerializedPropTypeName(targetProp); //���� �̸��� ���������� ã�����ϴ� Ÿ���� �ٸ� ������ �����ϱ����� Ÿ�� �̸�
        

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
                

                //�� array�� ������ ���
                if (typeof(IEnumerable).IsAssignableFrom(f.FieldType)) //f.FieldType�� IEnumerable���� Ȯ�� = IEnumerable�̶�� Ÿ�Ժ����� �� Ÿ���� ������ �� �ִ��� ���
                {
                    
                    
                    
                    Type nextType = f.FieldType.GetGenericArguments()[0];
                    
                    FieldInfo[] nextFields = nextType.GetFields(flags);
                    elementType = TryGetArrayElementFieldType(nextType, targetProp, flags);
                    
                    //��� 
                    //array�� ������� �ش� prop name�� �����ϴ��� Ȯ��
                }
            }
        }
        else
        {
           
            string fieldName = field.FieldType.ToString(); //field.GetType().ToString()�̶� �ٸ� ����� ��
            string [] names = fieldName.Split('.');
            int index = (names.Length - 1 == -1) ? 0 : (names.Length - 1); 
            string fieldTypeName = names[index]; //�������� ��ġ�� �ܾ �ʵ� Ÿ���� �̸��� ������
            
            if (typeName != fieldTypeName) //Property������ Type�� ���÷����� ���� ������ �̸��� �ʵ� Ÿ���� ������ Ȯ���� �ٸ� ��� �ٽ� ����
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
                        
                        //��� 
                        //array�� ������� �ش� prop name�� �����ϴ��� Ȯ��
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
            arraylist.InsertArrayElementAtIndex(lastindex); //�ӽ÷� property ���� �߰�
            SerializedProperty tempProp = arraylist.GetArrayElementAtIndex(lastindex); //������ ������Ƽ ������ ������
                ReadPropMeta(tempProp); //props�� ���� ������ �߰��� ������Ƽ(����)�� ������Ƽ ��θ� �̸� �˰��ְ� ���� �߰��� property�� Ÿ��, �̸����� �����Ѵ�.
            arraylist.DeleteArrayElementAtIndex(lastindex);
            readPropMeta = true; 
        }

        //�����ʹ� ������ �ϱ������� ������ �ѹ��� �ؾ��ϰ� �����Ѵ����� �ʱ�ȭ�� �ؼ� ���ο� ���� �Է¹޾Ƽ� Element�� �߰��� �� �־�ߵ�

        //������ �ʵ带 ����
        DrawFieldLayout(props);
        


        if (GUILayout.Button("Add"))
        {
            arraylist.InsertArrayElementAtIndex(lastindex); //property ���� �߰�
           
           

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
            updateWindow.SendEvent(EditorGUIUtility.CommandEvent("UpdateCategoryTree")); //���ο� ��Ұ� �߰����Ǹ� �� â �Ʒ��� �����ִ� parentWindow�� OnGUI ������ �ʱ�ȭ�ϰ� �ٽ� ����ǵ��� �ؾ���
           


            readPropMeta = false; 
            lastindex++;
            return;
            
        }
        if (GUILayout.Button("Cancel"))
        {
            readPropMeta = false;
            
        }

        




    }

    //Window�� ������
    private void OnDestroy()
    {
        
        //

    }




}
