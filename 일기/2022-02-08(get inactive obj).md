# inactive obj의 레퍼런스를 가져오는 방법

## Unity에서 제공하는 child 레퍼런스 탐색 방법
script에서 특정 오브젝트의 reference를 참조하여 값을 바꾸는 작업을 많이 하는데, 이때 unity에서 제공하는 오브젝트 참조 방법은 두가지가 존재한다.
1. transform.Find(string name): 부모 transform에서 name에 해당하는 child 오브젝트를 반환한다. 오로지 active 오브젝트만을 대상으로 하여 탐색한다.
2. transform.getchild(i): hierachy에서 부모 transform의 child list의 i번째에 해당하는 child 오브젝트를 반환한다. active - inactive 상태에 관계없이 반환한다.

## 문제점
Find의 장점은 이름으로 오브젝트를 찾을 수 있기때문에 정확하게 원하는 오브젝트의 레퍼런스를 받을 수 있다.   
하지만 inactive 오브젝트는 찾을 수 없다는 것이 큰 단점이다.   

getchild는 이름이 아닌 hierachy 상 child 오브젝트 위치(인덱스)로 접근하기때문에 hierachy에서 직접 위치를 정해주지않는 이상 원하는 오브젝트의 레퍼런스를 받기 어렵다.    
그 대신 inactive 오브젝트까지 대상으로 찾을 수 있다는 것이 장점이다.

## 해결 방안
active - inactive에 상관없이 오브젝트를 찾을 수 있다는 getchild의 장점을 이용하고 탐색하고자 하는 오브젝트의 이름과 탐색 결과 오브젝트의 이름이 일치하는지 확인하여
inactive 오브젝트의 레퍼런스를 이름으로 부터 찾을 수 있도록 한다.

```C# 

      public Transform FindMenuChild(Transform root , string searchPath)
    {

        Transform result = null;
        string[] substr = searchPath.Split('/');         //devide object path into object name and assign it
        string nextpath = "";

        
        //Process 1: make nextpath
        for(int i = 1; i < substr.Length; i++)
        {
            nextpath += substr[i];

            if(!(i == (substr.Length - 1))) //add '/' to right side of object name except last object name
                nextpath += '/';
            
            //nextpath has string except substr[0]
        }

        List<Transform> child = new List<Transform>();  //child saves root's children

        //Process 2: save children of root
        for(int i = 0; i < root.childCount; i++)
        {
            child.Add(root.GetChild(i));       //remember all child of root transform   
        }


        //Process 3: Checks if current process is under hierachy of target child's parent
        if (substr[0] == searchPath)    //when this is true, substr[0] is target object
        {
            
            for (int i = 0; i < child.Count; i++)   //find object that has target name
            {
                if (child[i].name == substr[0])
                {
                    
                    return child[i];    //return target
                }

            }

        }
       

        //Process 4: make recursive call from root to target's parent
        for (int i = 0; i < child.Count; i++)
        {
            if (child[i].name == substr[0])
            {
                bool isactive = child[i].gameObject.activeSelf; //remember state is whether active or inactive
                child[i].gameObject.SetActive(true);  
                result = FindMenuChild(child[i], nextpath);
                child[i].gameObject.SetActive(isactive);
            }

        }

        
        return result;  // if target were not found then return null
        

        


        //ex) obj search history: A/B/C/D = Str
        //1. string [] substr = Str.Split("/");
        //2. getchild는 active와 상관없이 순서에따라 child를 가져올 수 있음
    }

```

## 사용 예제
예를 들어, 
+ parent
  + child 1
    + child 2
      + child 3
         + target

다음과 같은 hierachy 구성에서 target의 transform을 참조하려고 한다면, root가 parent이므로 parent를 첫번째 인자로 전달한다.   
**이때 parent는 active object여야한다.** FindMenuChild(parent, child1/child2/child3/target) 이렇게 함수를 실행하면 target의 레퍼런스값을 알 수 있다.
