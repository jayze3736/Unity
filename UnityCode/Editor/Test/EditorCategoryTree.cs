using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorCategoryTree 
{

    public EditorCategoryNode root;
    //tree�� ����� node�� ���� 
    int nodecount = 0;
    //tree�� ĳ���� SerializedProperty�� ����
    int propcount = 0;
    public int NodeCount { get { return nodecount; } }
    public int PropCount { get { return propcount; } }



    public EditorCategoryTree()
    {
        root = new EditorCategoryNode();
    }

    /// <summary>
    /// rootProperty�� �о Tree�� �ʱ�ȭ�Ѵ�.
    /// </summary>
    /// <param name="rootProperty"></param>
    public EditorCategoryTree(SerializedProperty rootProperty)
    {
        root = new EditorCategoryNode();
        InitTree(rootProperty);
    }


    public void InitTree(SerializedProperty prop)
    {
        if (prop.isArray)
        {
            foreach (SerializedProperty element in prop)
            {
                SerializedProperty category = element.FindPropertyRelative("category");
                string path = category.stringValue;

                this.AddNode(path, root, element);


            }
        }

    }
    


    public void AddNode(string path, EditorCategoryNode node, SerializedProperty prop)
    {

        if (string.IsNullOrEmpty(path))
        {
            return;
        }


        //ex: path = a/b/c ���࿡ path�� empty���?
        int pos = path.IndexOf('/');
        string substr;  //a
        string subpath; //b/c
        if (pos >= 0)
        {
            substr = path.Substring(0, pos);  //a
            subpath = path.Substring((pos + 1)); //b/c
        }
        else
        {
            substr = path; //c �״��
            subpath = path; //c �״��
        }


        EditorCategoryNode nextnode = null;

        if (!node.Havechildren())  //count�� 0���� childlist�� �ش� node�� �ߺ��� ���� ����.
        {
            EditorCategoryNode newnode = new EditorCategoryNode(substr);
           
            node.AddChildren(newnode); //path�� �������������("") �ش� �̸��� ���� ��带 ã�� ���ϸ� �θ��� children���� �߰��Ѵ�.
            nodecount++;
            nextnode = newnode;
            
        }
        else
        {
            bool isexist = false;
            foreach (EditorCategoryNode child in node.children)
            {
                if (child.name == substr) //�̹� �����ϴ� ��쿡
                {
                   
                    nextnode = child;
                    isexist = true;
                    break;
                }
            }
            if (!isexist)
            {
                //��带 ���� ����
                EditorCategoryNode newnode = new EditorCategoryNode(substr);
                node.AddChildren(newnode); //path�� �������������("") �ش� �̸��� ���� ��带 ã�� ���ϸ� �θ��� children���� �߰��Ѵ�.
                nodecount++;
                nextnode = newnode;
               
            }



        }


        
        if (substr == subpath || string.IsNullOrEmpty(substr))   //���̻� �и��� �� ������
        {
            nextnode.AddProperty(prop);
            propcount++;
            return;
            //������ ��尡 ���� branch�� �����ϴ��� Ȯ���ؾ��� = parent�� childlist�� �ش� ��尡 �ִ��� Ȯ���ؾ���
        }
        AddNode(subpath, nextnode, prop);



    }





}
