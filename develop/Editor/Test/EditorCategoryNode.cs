using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorCategoryNode
{
    public string name;

    public List<EditorCategoryNode> children;
    
    public List<SerializedProperty> properties;
    
    

    public EditorCategoryNode(string name)
    {
        this.name = name;
        children = new List<EditorCategoryNode>();
        properties = new List<SerializedProperty>();
        
    }
    public EditorCategoryNode()
    {
        children = new List<EditorCategoryNode>();
        properties = new List<SerializedProperty>();
        
    }

    public void AddProperty(SerializedProperty p)
    {
        properties.Add(p);
    }

    public List<SerializedProperty> GetProperties()
    {
        return properties;
    }

    public void AddChildren(EditorCategoryNode child)
    {
        children.Add(child);
    }

    /// <summary>
    /// �� ��尡 Property�� �����ϰ��ִ°� 
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return (properties.Count == 0) || (properties == null);
    }

    /// <summary>
    /// �� ��尡 �ڽ� ��带 ������ �ִ°�
    /// </summary>
    /// <returns></returns>
    public bool Havechildren()
    {
        return (children.Count != 0) && (children != null);
    }

}
