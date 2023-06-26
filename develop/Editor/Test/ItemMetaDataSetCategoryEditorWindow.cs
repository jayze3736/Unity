using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PropertyStyleContainer
{
    int index;
    List<GUIStyle> btnstyles = null;

    public PropertyStyleContainer()
    {

    }

    public PropertyStyleContainer(int count, GUIStyle style)
    {

        index = 0;
        if (btnstyles == null)
        {
            btnstyles = new List<GUIStyle>();
            while (btnstyles.Count < count)
            {
                GUIStyle btnstyle = new GUIStyle(style); //�ּҴ� �ٸ����� ��Ÿ���� ���� ������ ����
                btnstyles.Add(btnstyle);
            }

        }

    }

    public void Reset()
    {
        index = 0;
    }

    public GUIStyle GetNextStyleElement()
    {

        if (btnstyles == null)
        {
            return null;
        }

        return btnstyles[index++];
    }

    public int GetIndex()
    {
        return index;
    }

    
    







}



public class ItemMetaDataSetCategoryEditorWindow : ExtendedEditorWindow
{

    GUIStyle GetBtnStyle()
    {
        var s = new GUIStyle(GUI.skin.button);
        var b = s.border;
        
        s.fontStyle = FontStyle.Normal;
        
        s.fontSize = 12;
        b.left = 0;
        b.top = 0;
        b.right = 0;
        b.bottom = 0;
        s.normal.background = Texture2D.grayTexture;
        s.stretchWidth = false;
        return s;
       
    }

    



    /// <summary>
    /// ������ ������Ƽ�� ��Ÿ�� ��ü�� �ּҸ� ����Ŵ
    /// </summary>
    GUIStyle selectedPropertyStyle;


    Stack<EditorWindow> openedSubWindows = new Stack<EditorWindow>();

    /// <summary>
    /// �� Window�� ������ ó���� �̺�Ʈ ������
    /// </summary>
    Action<EditorWindow> onCloseWindow;

    /// <summary>
    /// ������ Element�� ��Ÿ���� �����Ͽ� ������ ����� �������� �ٲٱ� ���� ����
    /// </summary>
    PropertyStyleContainer container;

    /// <summary>
    /// Category Tree�� Foldout ���¸� �����ϱ� ���� ����
    /// </summary>
    List<bool> IsExpanded = new List<bool>();

    /// <summary>
    /// ScrollView�� ���� ����
    /// </summary>
    Vector2 scrollpos;
    /// <summary>
    /// sub window�� ���� �����Ͱ� ����ɰ�� updateTree�� true�� ����� Tree�� ������Ʈ�ϰ� �� Tree�� ���� ���� �����Ǵ� �����͵��� ������Ʈ�Ѵ�.
    /// </summary>
    bool updateTree;

    public static void Open(CustomItemMetaDataSet container)
    {

        ItemMetaDataSetCategoryEditorWindow window = GetWindow<ItemMetaDataSetCategoryEditorWindow>("Item Meta Data Editor");
        window.serializedObject = new SerializedObject(container);

    }

    
    /// <summary>
    /// root ��带 �������� GUI layout�� Window�� �ۼ��Ѵ�.
    /// </summary>
    /// <param name="node"> ���� ��� </param>
    /// <param name="startindex"> ������ų IsExpanded�� ���� �ε��� </param>
    /// <param name="defaultWidth"> Foldout�� width </param>
    /// <param name="indentwidth"> Foldout�� ���� ���� ũ�� </param>
    /// <param name="space"> Property ��ư�� ���� ũ�� </param>
    /// <returns></returns>
    public int DrawCategoryTree(EditorCategoryNode node, int startindex, int defaultWidth, int indentwidth, int space)
    {
        
        if (startindex >= IsExpanded.Count)
        {
            return -1;
        }

        
        int nextindex = startindex;

        foreach (var child in node.children)
        {
            
            GUILayout.BeginHorizontal(GUILayout.Width(defaultWidth)); //�������� �ٲ�
                IsExpanded[nextindex] = EditorGUILayout.Foldout(IsExpanded[nextindex], child.name);    //draw foldable category label
            GUILayout.EndHorizontal();

            int curindex = nextindex; //foldout�� ���¿� ���� ������ bool ����Ʈ������ ���� �ε���
            nextindex++; //isExpanded ���ο� �ϴ� index�����ϴ� �� ������ �ȵ�

            //Foldout�� ��������
            if (IsExpanded[curindex])
            {
                //Property�� ������ ���
                if (!child.IsEmpty())
                {
                    
                    foreach(var p in child.GetProperties())
                    {
                        var nameProp = p.FindPropertyRelative("name");
                        if(nameProp == null)
                        {
                            Debug.LogError("There is no name property, Add property so that name can be shown on EditorWindow");
                        }
                        else
                        {
                            string name = nameProp.stringValue;
                            GUILayout.BeginHorizontal(); //��ư�� �⺻������ vertical element�̳�, horizontal group���� �����ϰ� space�� �ָ� ����������� ������ ����
                                GUILayout.Space(space);
                                GUIStyle style = container.GetNextStyleElement();
                           
                                //�����̳ʿ��� ���� ��Ÿ���� ���� ��Ÿ�� ��ü�� �ּҰ� ������ ��� �ش� ��Ÿ���� �ٲ�
                                if(selectedPropertyStyle == style)
                                {
                                    style.normal.textColor = Color.red;
                                    
                                }
                                else
                                {
                                    style.normal.textColor = Color.white;
                                    
                                }
                            
                                //������Ʈ�� ��ư�� Ŭ���Ͽ� ��ȸ����
                                if (GUILayout.Button(name, style))
                                {
                                    //p�� ���������� �׳� selectedProperty = p �� ȣ���ϸ� p�� ������ ���ɼ��� �ְ� selectedProperty�� p�� ������ �Ҿ���� �� ����
                                    selectedPropertyPath = p.propertyPath;
                                    selectedPropertyStyle = style;
                                    
                                    
                                }

                                if (!string.IsNullOrEmpty(selectedPropertyPath))
                                {
                                    selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
                                }

                            GUILayout.EndHorizontal();
                        }
                        

                        

                        //Draw label with property name
                    }
                    

                    //���࿡ ���̺��� Ŭ���Ǿ Property�� �׷��ߵ� ���, Queue���ٰ� ����ְ� DrawCategoryTree�� ȣ���� �������� �ش� Queue�� �ִ� ���� ������ �׸���
                    
                }


                //���� ��忡 �ڽ� ��尡 �޷��������
                if (child.Havechildren()) 
                {
                    EditorGUI.indentLevel++;
                    
                    int returnindex = DrawCategoryTree(child, nextindex, defaultWidth + indentwidth, indentwidth, indentwidth + space); 
                    
                    nextindex = returnindex;
                    EditorGUI.indentLevel--;
                }

              

            }
            else //Foldout�� ��������
            {
                
                int returnindex = GetNextIndexFromCategoryTree(child, nextindex); //foldout�� �����־ ���� index���� �ޱ����ؼ� Tree�� Traverse�� �� �ִ� �Լ��� ȣ��
                nextindex = returnindex;
                
            }
            

            

        }
        return nextindex;
        



       

    }


    /// <summary>
    /// Foldout State�� false�϶� �����ϴ� bool list�� index���� �����ϱ����ؼ� Foldout�� �׸����ʰ� �ε��������� ����Ͽ� ��ȯ
    /// </summary>
    /// <param name="node"></param>
    /// <param name="startindex"></param>
    /// <returns></returns>
    public int GetNextIndexFromCategoryTree(EditorCategoryNode node, int startindex)
    {
        int nextindex = startindex;
        if (node.Havechildren())
        {
            foreach (var child in node.children)
            {
                nextindex++;
                int result = GetNextIndexFromCategoryTree(child, nextindex);
                nextindex = result;
            }
        }

        return nextindex;
    }


    public void OnCloseWindow(EditorWindow window)
    {

        var handler = onCloseWindow;

        if(handler != null)
        {
            handler(window);
            
        }

    }

    public void CloseEditorWindow(EditorWindow window)
    {
        window.Close();
    }

    /// <summary>
    /// Delete ���
    /// </summary>
    /// <param name="arrayProp"> current </param>
    /// <param name="selectedProperty"></param>
    public void DeleteSelectedElement(SerializedProperty arrayProp, SerializedProperty selectedProperty)
    {
        
        //���� ������ ������Ƽ�� ����Ʈ���� ���°�� ��ġ�ϴ��� ��ȸ�Ѵ�
        int selectedPropIndex = FindSelectedPropertyIndex(arrayProp, selectedProperty);
        
        if (selectedPropIndex != -1 && arrayProp.isArray)
        {

            arrayProp.DeleteArrayElementAtIndex(selectedPropIndex);
            this.selectedProperty = null;
            
        }
    }




    private void Awake()
    {
        
    }

    public int FindSelectedPropertyIndex(SerializedProperty arrayProp, SerializedProperty target)
    {
        if (!arrayProp.isArray)
        {
            Debug.Log("arrayProp is not array");
            throw new ArgumentException();
            
        }
        else if(arrayProp == null)
        {

            Debug.Log("arrayProp is null");
            throw new NullReferenceException();
            
        }
        else if (target == null)
        {

            Debug.Log("target is null");
            throw new NullReferenceException();
            
        }
        int index = 0;

        foreach(SerializedProperty element in arrayProp)
        {
            
            if (SerializedProperty.EqualContents(element, target))  //��Ȯ���� �갡? 1. EqualContents(���Ⱑ ����) �ƴϸ� target�� ���̻� currentProperty�� ���� ���� ���� 2. GetArrayElementAtIndex����(�ƴ�) 3. SelectedProperty���� 
            {
                return index;
            }
            index++;


        }

      

        Debug.Log("target is not found");
        return -1;
    }

    


    public bool GetEventMessage(string eventName)
    {
        Event e = Event.current;
        return (e.commandName == eventName);
        
    }

    private void OnDestroy()
    {
        
        while(openedSubWindows.Count != 0)
        {
            var window = openedSubWindows.Pop();
            window.Close();
        }

    }


    public void OnGUI()
    {
        updateTree = false;
        serializedObject.Update();



        if (GetEventMessage("UpdateCategoryTree"))
        {
            updateTree = true;
        }

        

        currentProperty = serializedObject.FindProperty("dataSet");

        EditorCategoryTree categoryTree = new EditorCategoryTree(currentProperty);


        

        if (container == null || updateTree)
        {
            container = new PropertyStyleContainer(categoryTree.PropCount, GetBtnStyle());
            updateTree = false;
            if (updateTree)
            {
                return;
            }
        }

       


        //Create bool variables for remebering foldout state(���� �ƿ� ���¸� ����ϱ� ���� �̸� bool ����Ʈ�� �ʱ�ȭ)
        while (IsExpanded.Count < categoryTree.NodeCount)
        {
            IsExpanded.Add(true);
        }


        EditorGUILayout.BeginVertical("box", GUILayout.MaxHeight(20));

        EditorGUILayout.BeginHorizontal();



       
        if (GUILayout.Button("Add Element"))
        {
            if (!EditorWindow.HasOpenInstances<AddNewPropertyElementWindow>())
            {
                EditorWindow subWindow = AddNewPropertyElementWindow.Open(currentProperty, serializedObject);
                openedSubWindows.Push(subWindow);
            }




        }
        if (GUILayout.Button("Delete Element"))
        {
            try
            {

                DeleteSelectedElement(currentProperty, selectedProperty);
                Apply();
                return;

            }
            catch
            {
                Debug.LogError("Failed to Delete Element");
            }

        }

        //button �ΰ� - 1. add element, 2. delete element

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        

        EditorGUILayout.BeginHorizontal();


        
        //Category Tree�� Window�� �ۼ�
        EditorGUILayout.BeginVertical("box",GUILayout.MaxWidth(300), GUILayout.ExpandHeight(true));
                scrollpos = EditorGUILayout.BeginScrollView(scrollpos ,true, true);
                    DrawCategoryTree(categoryTree.root, 0, 100, 20, 20); 
        EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

        container.Reset();
        
        // ������ ������Ƽ�� �����ش�.
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
                if (selectedProperty != null)
                {
                    selectedProperty = DrawProperties(selectedProperty, true);
                }
            EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        Apply();





    }

}
