+ 데이터 바인딩 예제 출처: https://docs.unity3d.com/Manual/UIE-Binding.html   
+ Visual Tree: https://docs.unity3d.com/Manual/UIE-VisualTree.html   
+ Text field: https://docs.unity3d.com/Packages/com.unity.ui@1.0/api/UnityEngine.UIElements.TextField.html?q=textfield   
+ Bind(): https://docs.unity3d.com/Packages/com.unity.ui@1.0/api/UnityEditor.UIElements.BindingExtensions.html#UnityEditor_UIElements_BindingExtensions_Bind_UnityEngine_UIElements_VisualElement_SerializedObject_
+ VisualElement: https://docs.unity3d.com/Packages/com.unity.ui@1.0/api/UnityEngine.UIElements.VisualElement.html?q=visualelement   
+ Selection 클래스 예제: https://popbox.tistory.com/107   

```C#
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UIElementsExamples
{
    public class SimpleBindingExample : EditorWindow
    {
        TextField m_ObjectNameBinding;
        

        [MenuItem("Window/UIElementsExamples/Simple Binding Example")]
        public static void ShowDefaultWindow()
        {
            var wnd = GetWindow<SimpleBindingExample>();
            wnd.titleContent = new GUIContent("Simple Binding");
        }

        public void OnEnable()
        {
            var root = this.rootVisualElement;
            m_ObjectNameBinding = new TextField("Object Name Binding");
            
            Toggle t = new Toggle("toggle object");
            
            //bindingPath는 바인딩할 프로퍼티를 설정하는 string 필드
            t.bindingPath = "m_IsActive";   //YAML에 정의된 프로퍼티중 isactive 값을 바인딩할 것으로 선언
            m_ObjectNameBinding.bindingPath = "m_Name"; //오브젝트 name값을 바인딩할 것으로 선언
            
            root.Add(m_ObjectNameBinding);  //textfield를 추가
            
            root.Add(t);   //toggle 값을 추가
            OnSelectionChange();
        }

        public void OnSelectionChange()
        {
            //Selection은 Hierachy(editor)에서 선택한 오브젝트(들)에 대한 클래스를 의미
            //Selection.activeObject는 active하고 선택한 오브젝트 한 개를 의미
            GameObject selectedObject = Selection.activeObject as GameObject;
            if (selectedObject != null)
            {
                // SerializedObject는 주소값만 다르고 동일한 메타데이터를 가지고 있는 파일들을 직렬화하여 한번에 관리하는 것을 가능케하기 위한 클래스
                // 하지만 여기서는 단순히 rootVisualElement에 bind하기위해서 사용하는 듯 함

                SerializedObject so = new SerializedObject(selectedObject);
                // Bind it to the root of the hierarchy. It will find the right object to bind to...
                //rootVisualElement.Bind(so);

                // ... or alternatively you can also bind it to the TextField itself.


                
                m_ObjectNameBinding.Bind(so);   //선택한 so 오브젝트는 bind된 오브젝트네임바인딩의 textfield내부값을 따라감
                
                m_ObjectNameBinding.value = "what?";  //hierachy에 선택한 오브젝트의 이름을 what?으로 변경(선택시 변경됨)
                //m_ObjectNameBinding.value = "how?";
                //m_ObjectNameBinding.name = "what?";


            }
            else
            {
                // Unbind the object from the actual visual element
                //rootVisualElement.Unbind();

                 m_ObjectNameBinding.Unbind();

                // Clear the TextField after the binding is removed
                m_ObjectNameBinding.value = "";
            }
        }
    }
}

```
