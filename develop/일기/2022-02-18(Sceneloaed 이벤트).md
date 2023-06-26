## Onenable이라는 함수는 Scene이 새로이 load 될때마다 호출이 되는가?
그렇지 않다. 대신 scene이 loaded될때마다 이벤트 호출은 sceneloaded로 가능

## sceneloaded의 특징
sceneloaded에 이벤트를 가입시키면 다음 scene에서 해당 오브젝트가 hierachy에 없어도 가입된 해당 이벤트를 실행시킬 수 있다.
   
   (scene이 이동할때 오브젝트가 destroy되어도 C# 가비지 콜렉터에서는
아직 해당 객체가 회수가 되지않아서 이벤트를 실행시킬 수 있는 것 같다.)
+ 전에 들은 적이  있는데 unity에서 destroy가 이루어져도 C#에서 아직 객체가 사용되고 있으면 가비지 콜렉터에서 수집하지않기때문에
가입되어있는 이벤트를 사용할 수 있는 것 같다.
