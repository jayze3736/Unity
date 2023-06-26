DontDestroyOnLoad()와 씬간의 오브젝트 이동시 address(주소)변경에 관하여
======================================================================

1. DontDestroyOnLoad() 함수란

  DontDestroyOnLoad()함수의 인자로 입력된 오브젝트는 다른 씬으로 이동되도 해당 오브젝트를 유지하는 것이 가능하다.
  
  참고: https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
  ***
  
2. 씬 이동시 Awake()
  
  DontDestroyOnLoad()로 다음 씬 이동시 같이 유지되는 오브젝트들은 이동 전 씬에서 Awake()가 실행되어도 이동 후에 Awake()가 실행된다.
  ***

3. DontDestroyOnLoad() 함수 이용시 주소 변경?

  Unity에서 오브젝트마다 고유한 ID값을 가지고 있는데, 이는 GetInstaceID() 함수로 파악이 가능하며 주소 대신에 같은 속성의 객체를 구별하는데 이용할 수 있다.
  다음과 같은 코드를 살펴보자.
  
 AudioManager.cs
 <pre>
 <code>
        public static AudioManager instance;
        public void Awake()
       {
        


        if(instance != null)
        {

            print(this.GetInstanceID());

            
            if (instance != this)
            {
                
                Destroy(instance.gameObject);
            }

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            

        }

        print(this.GetInstanceID());


    }
    
   </code>
   </pre>

  해당 코드는 싱글톤 패턴을 이용하고 있으며, 인스턴스를 참조하는 과정을 나타내고 있다. 처음 else문으로 빠져나와 이 오브젝트(this)가 다음 씬으로 이동되어도 삭제되지않고, instance에는 this가
  참조된다.
  그러나 this.GetInstanceID()로 확인해본 결과, 씬 이동전과 이동후 다른 주소를 가지고 있었다.
  
![제목 없음](https://user-images.githubusercontent.com/79313194/112755831-6e112200-901d-11eb-937f-ef4aae39be41.png)

  17918이 씬 이동전 ID이고 28586이 이동후 ID이다. 따라서 만약에 if(instance != null) -> if(instance != this)가 참이되고 따라서 instance가
  참조하고 있는 오브젝트는 파괴된다. 따라서 씬 이동으로 오브젝트가 유지되어도 해당 코드로 인해 파괴되어 원치않는 상황이 발생한다.

  
