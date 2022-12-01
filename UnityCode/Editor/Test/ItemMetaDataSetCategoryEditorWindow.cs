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
                GUIStyle btnstyle = new GUIStyle(style); //주소는 다르지만 스타일은 같은 변수를 생성
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
    /// 선택한 프로퍼티의 스타일 객체의 주소를 가리킴
    /// </summary>
    GUIStyle selectedPropertyStyle;


    Stack<EditorWindow> openedSubWindows = new Stack<EditorWindow>();

    /// <summary>
    /// 이 Window가 닫힐때 처리할 이벤트 리스너
    /// </summary>
    Action<EditorWindow> onCloseWindow;

    /// <summary>
    /// 선택한 Element의 스타일을 저장하여 선택한 요소의 디자인을 바꾸기 위한 변수
    /// </summary>
    PropertyStyleContainer container;

    /// <summary>
    /// Category Tree의 Foldout 상태를 저장하기 위한 변수
    /// </summary>
    List<bool> IsExpanded = new List<bool>();

    /// <summary>
    /// ScrollView를 위한 변수
    /// </summary>
    Vector2 scrollpos;
    /// <summary>
    /// sub window에 의해 데이터가 변경될경우 updateTree를 true로 만들어 Tree를 업데이트하고 이 Tree에 의해 값이 결정되는 데이터들을 업데이트한다.
    /// </summary>
    bool updateTree;

    public static void Open(CustomItemMetaDataSet container)
    {

        ItemMetaDataSetCategoryEditorWindow window = GetWindow<ItemMetaDataSetCategoryEditorWindow>("Item Meta Data Editor");
        window.serializedObject = new SerializedObject(container);

    }

    
    /// <summary>
    /// root 노드를 시작으로 GUI layout을 Window에 작성한다.
    /// </summary>
    /// <param name="node"> 시작 노드 </param>
    /// <param name="startindex"> 참조시킬 IsExpanded의 시작 인덱스 </param>
    /// <param name="defaultWidth"> Foldout의 width </param>
    /// <param name="indentwidth"> Foldout의 띄어쓰기 공백 크기 </param>
    /// <param name="space"> Property 버튼의 공백 크기 </param>
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
            
            GUILayout.BeginHorizontal(GUILayout.Width(defaultWidth)); //동적으로 바꿈
                IsExpanded[nextindex] = EditorGUILayout.Foldout(IsExpanded[nextindex], child.name);    //draw foldable category label
            GUILayout.EndHorizontal();

            int curindex = nextindex; //foldout의 상태에 따라 제어할 bool 리스트에서의 변수 인덱스
            nextindex++; //isExpanded 내부에 일단 index증가하는 걸 넣으면 안됨

            //Foldout이 열렸을때
            if (IsExpanded[curindex])
            {
                //Property가 존재할 경우
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
                            GUILayout.BeginHorizontal(); //버튼은 기본적으로 vertical element이나, horizontal group으로 지정하고 space를 주면 수평방향으로 공백이 생김
                                GUILayout.Space(space);
                                GUIStyle style = container.GetNextStyleElement();
                           
                                //컨테이너에서 받은 스타일이 현재 스타일 객체와 주소가 동일한 경우 해당 스타일을 바꿈
                                if(selectedPropertyStyle == style)
                                {
                                    style.normal.textColor = Color.red;
                                    
                                }
                                else
                                {
                                    style.normal.textColor = Color.white;
                                    
                                }
                            
                                //엘리먼트는 버튼을 클릭하여 조회가능
                                if (GUILayout.Button(name, style))
                                {
                                    //p가 지역변수라서 그냥 selectedProperty = p 를 호출하면 p가 삭제될 가능성이 있고 selectedProperty가 p의 참조를 잃어버릴 수 있음
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
                    

                    //만약에 레이블이 클릭되어서 Property를 그려야될 경우, Queue에다가 집어넣고 DrawCategoryTree가 호출이 끝난다음 해당 Queue에 있는 값을 꺼내서 그리기
                    
                }


                //현재 노드에 자식 노드가 달려있을경우
                if (child.Havechildren()) 
                {
                    EditorGUI.indentLevel++;
                    
                    int returnindex = DrawCategoryTree(child, nextindex, defaultWidth + indentwidth, indentwidth, indentwidth + space); 
                    
                    nextindex = returnindex;
                    EditorGUI.indentLevel--;
                }

              

            }
            else //Foldout이 닫혔을때
            {
                
                int returnindex = GetNextIndexFromCategoryTree(child, nextindex); //foldout이 접혀있어도 원래 index값을 받기위해서 Tree를 Traverse할 수 있는 함수를 호출
                nextindex = returnindex;
                
            }
            

            

        }
        return nextindex;
        



       

    }


    /// <summary>
    /// Foldout State가 false일때 참조하는 bool list의 index값을 유지하기위해서 Foldout을 그리지않고 인덱스값만을 계산하여 반환
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
    /// Delete 기능
    /// </summary>
    /// <param name="arrayProp"> current </param>
    /// <param name="selectedProperty"></param>
    public void DeleteSelectedElement(SerializedProperty arrayProp, SerializedProperty selectedProperty)
    {
        
        //현재 선택한 프로퍼티가 리스트에서 몇번째에 위치하는지 조회한다
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
            
            if (SerializedProperty.EqualContents(element, target))  //정확히는 얘가? 1. EqualContents(여기가 문제) 아니면 target이 더이상 currentProperty에 없을 수도 있음 2. GetArrayElementAtIndex에서(아님) 3. SelectedProperty에서 
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

       


        //Create bool variables for remebering foldout state(폴드 아웃 상태를 기억하기 위해 미리 bool 리스트를 초기화)
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

        //button 두개 - 1. add element, 2. delete element

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        

        EditorGUILayout.BeginHorizontal();


        
        //Category Tree를 Window에 작성
        EditorGUILayout.BeginVertical("box",GUILayout.MaxWidth(300), GUILayout.ExpandHeight(true));
                scrollpos = EditorGUILayout.BeginScrollView(scrollpos ,true, true);
                    DrawCategoryTree(categoryTree.root, 0, 100, 20, 20); 
        EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

        container.Reset();
        
        // 선택한 프로퍼티를 보여준다.
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
