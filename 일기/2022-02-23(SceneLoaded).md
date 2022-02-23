# SceneManager.sceneLoaded의 생명주기 순위

## ScneneManager.sceneLoaded
SceneManager.sceneLoaded는 이벤트 핸들러이며 가입한 이벤트가 씬 로드후에 발생하도록 한다.   
   public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;   
   기본적으로 핸들러에 가입시킬 함수의 파라미터 또한 Scene, LoadeSceneMode로 맞춰야하는데, 어째서인지 파라메터에 관계없이 함수를 delegate로 wrap(delegate 블록 안에 함수를 넣음)
   시키면 해당 이벤트가 가입될 수 있다.

## 생명주기
유니티내에서 실행되는 이벤트 함수는 Awake -> Onenable -> Start 순으로 실행된다.   
참고: https://docs.unity3d.com/kr/530/Manual/ExecutionOrder.html

Scene A -> Scene B로 씬 전환이 발생할때
Scene A에서 SceneLoaded 로 함수를 가입시켰을때 Scene B에서 순서대로 Awake(), OnEnable(), Start()가 실행될때 가입시킨 이벤트는 생명주기내 언제 발생하는지 궁금했다.

## 확인
Scene A에 obj A를, Scene B에 obj B를 놓고 obj A에서 sceneLoaded event가 발생했음을 알리는 로그를 출력하는 이벤트를 가입시킨다.   
obj B는 Awake(), OnEnable(), Start() 각각 실행 지점을 진입했다는 로그를 출력하도록 한다.

### obj A의 경우
``` c#
testbutton.onClick.AddListener(SceneManagerScript.LoadNextScene);
SceneManager.sceneLoaded += delegate { Debug.Log("SceneManager event raised"); };
```

### obj B의 경우
```c#

        private void Awake()
        {
            
            Debug.Log("Awake");
            
        }



        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("start");
        }


        private void OnEnable()
        {
            Debug.Log("Onenable");
        }

        // Update is called once per frame
        void Update()
        {


        }
```

## 결과
![제목 없음](https://user-images.githubusercontent.com/79313194/155293789-89b90506-893e-452f-933c-92f279fd7090.png)

***Awake() -> OnEnable() -> SceneLoaded event -> Start() 순으로 실행됨을 확인할 수 있었다.***   
결론적으로 SceneLoaded event가 로딩중인 씬의 오브젝트를 dependency로 가질때 이벤트가 실행되기전인 Awake 또는 OnEnable에서 해당 오브젝트의 reference를
정해주면 이벤트 핸들러가 오브젝트 참조에 성공할 것이다.

