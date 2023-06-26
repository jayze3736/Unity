# OnEnable, Start, Awake의 실행순서에 대하여
## OnEnable, Start, Awake란?
유니티 생명주기라고 구글에 검색해보면 이미지로 유니티의 이벤트 실행순서를 확인할 수 있다.
Awake는 씬 오브젝트가 로드된 직후에 실행이되며 한번만 실행된다.  
OnEnable 또한 씬이 오브젝트 로드 직후에 실행이되는데, Awake와 다르게 오브젝트가 활성화(active)될때마다 실행된다.  
Start는 OnEnable과 Awake보다 나중에 실행되며 프레임 업데이트가 이루어지는 시점에 1번만 실행된다.  
 
## 실행순서
Awake - OnEnable - Start 순으로 실행된다고 써져있는데, 주의해야할 점이 있다. 이 실행순서는 모든 오브젝트에 대해서 공통적으로 정해진 순서가 아니다. 다시 말해서
A 오브젝트의 스크립트 내에서는 Awake - OnEnable - Start 순으로 함수가 실행이 되지만 B 오브젝트가 존재하고 생명주기 함수가 실행될때 A오브젝트의 OnEnable이 B오브젝트의 Awake보다
먼저 실행될 수 있다.

## 실험
빈 게임오브젝트 test2, test3를 생성한다.   
![1](https://user-images.githubusercontent.com/79313194/170852384-c93070bb-d72e-4827-a2ca-563531ca7d52.png)

동일한 내용의 스크립트를 각 오브젝트에 부착한다.
test2.cs, test3.cs
``` c#
private void OnEnable()
    {
        Debug.Log(this.transform.name + "OnEnable Executed");
    }

    private void Awake()
    {
        Debug.Log(this.transform.name + "Awake Executed");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.name + "Start Executed");
        
    }
```
    
### 결과
![2](https://user-images.githubusercontent.com/79313194/170852404-92726f92-6052-4cb5-8ded-274cd0db7f53.png)

- test3의 오브젝트내에서는 Awake - OnEnable 순으로 우리가 알고있는 생명주기 순서로 실행되었지만 test3의 OnEnable이 test2의 Awake 함수보다 먼저 실행된 것을 확인할 수 있다.
- Start 함수는 무조건 Awake, OnEnable 함수뒤에 실행되기때문에 두 오브젝트의 Start 함수는 두 오브젝트의 Awake와 OnEnable 함수보다 나중에 실행되는 것을 확인할 수 있다.

## 고찰
1. Awake - OnEnable 순서는 한 오브젝트 내에서는 유효하지만 다른 오브젝트에서 동일한 순서를 공유하지않는다는 것
2. 어떠한 초기화작업에서 순서가 필요한 작업은 Awake와 OnEnable 조합이 아니라 Awake - Start 또는 OnEnable - Start 조합으로 해야한다.
3. Start 함수는 Awake 와 OnEnable 호출이후 실행된다는 규칙(또는 순서)을 모든 오브젝트가 공유한다.

