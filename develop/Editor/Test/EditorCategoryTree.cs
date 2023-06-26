using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorCategoryTree 
{

    public EditorCategoryNode root;
    //tree에 저장된 node의 개수 
    int nodecount = 0;
    //tree에 캐쉬된 SerializedProperty의 개수
    int propcount = 0;
    public int NodeCount { get { return nodecount; } }
    public int PropCount { get { return propcount; } }



    public EditorCategoryTree()
    {
        root = new EditorCategoryNode();
    }

    /// <summary>
    /// rootProperty를 읽어서 Tree를 초기화한다.
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


        //ex: path = a/b/c 만약에 path가 empty라면?
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
            substr = path; //c 그대로
            subpath = path; //c 그대로
        }


        EditorCategoryNode nextnode = null;

        if (!node.Havechildren())  //count가 0개인 childlist에 해당 node가 중복될 경우는 없다.
        {
            EditorCategoryNode newnode = new EditorCategoryNode(substr);
           
            node.AddChildren(newnode); //path가 비어있지않은데("") 해당 이름을 가진 노드를 찾지 못하면 부모의 children으로 추가한다.
            nodecount++;
            nextnode = newnode;
            
        }
        else
        {
            bool isexist = false;
            foreach (EditorCategoryNode child in node.children)
            {
                if (child.name == substr) //이미 존재하는 경우에
                {
                   
                    nextnode = child;
                    isexist = true;
                    break;
                }
            }
            if (!isexist)
            {
                //노드를 새로 생성
                EditorCategoryNode newnode = new EditorCategoryNode(substr);
                node.AddChildren(newnode); //path가 비어있지않은데("") 해당 이름을 가진 노드를 찾지 못하면 부모의 children으로 추가한다.
                nodecount++;
                nextnode = newnode;
               
            }



        }


        
        if (substr == subpath || string.IsNullOrEmpty(substr))   //더이상 분리될 수 없을때
        {
            nextnode.AddProperty(prop);
            propcount++;
            return;
            //마지막 노드가 현재 branch에 존재하는지 확인해야함 = parent의 childlist에 해당 노드가 있는지 확인해야함
        }
        AddNode(subpath, nextnode, prop);



    }





}
