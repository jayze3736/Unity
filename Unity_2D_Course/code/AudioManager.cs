using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;

    public bool Isloop = false;

    public AudioClip clip;
    private AudioSource source;
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.loop = Isloop;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

}


public class AudioManager : MonoBehaviour
{
    /* AudioSource는 Inspector에서도 볼 수 있으며 AudioClip을 포함하고 있고, volume과 mute등 AudioClip을
     * 편집하여 재생하도록 할 수 있다.
     * 
     * TODO1: Sound 클래스 array를 생성하고 이 array 요소를 편집할 수 있도록 함
     * 
     * 스태틱 변수인 instance가 있다고 해도 inspector로 Sound array를 각 요소마다 편집을 하게되면
     * 해당 array 요소가 instance의 멤버 변수 즉, sounds가 아니므로 이를 연결해줄 필요가 있음
     * inspector에서 보여지는 sounds는 inspector에 추가되는 component인 AudioManager class의 멤버 변수이므로
     * instance에는 this(AudioManger Component)를 참조해야한다.
     * 
     * 
     * clip을 재생하려면 AudioSource가 필요함 따라서 편집은 AudioManager Component에서 진행하고, 실제로 재생을 하기 위해서는
     * AudioSource Component가 필요하므로 GameObject를 따로 생성하여 사용할 수 있도록 함
     * 
     * 정리:
     * 1. Clip을 재생하기 위해서는 AudioSource 컴포넌트가 필요하므로 각 Sound 배열
     *  요소값에 대응되는 GameObject를 생성하여 해당 컴포넌트를 추가시킨다.
     * 2. Sounds에는 SetSource 함수가 존재하는데, 이는 Inspector에서 지정한 Clip과 GameObject의 AudioSource로 Sounds의
     * Clip과 AudioSource값으로 설정한다.
     * 3. Audio의 Volume과 Pitch는 Inspector에서 지정할 수 있다. Sounds의 volume과 pitch가 public 속성이기 때문이다.
     * 4. AudioManager의 PlaySound 함수는 이름명을 인자로하여 해당 인자와 같은 명의 Sound clip이 있으면 그 클립을 재생하도록 하는
     * 함수이다.
     * 5. instance가 스태틱 변수이므로 어떤 스크립트에서도 접근이 가능하기때문에 어디에서든 이름명을 매개로하여 사운드를 재생할 수
     * 있다. (원래 사운드를 재생하기 위해서는 Clip을 일일히 부착하여 Play()함수를 사용해야했다. 따라서, 
     * 서로 다른 오브젝트에서 사운드를 일일히 수정하기보다 한 오브젝트에서 관리하기위해 AudioManager 클래스를 만들었다.)
     */

    public static AudioManager instance;

    [SerializeField]
    private Sound[] sounds;

    public void Awake()
    {
        if (instance != null)
        {


            if (instance == this)
            {

                Destroy(instance.gameObject);
            }

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);


        }


    }


    // Start is called before the first frame update
    void Start()
    {
        
        Sinit();
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySound(string _name)
    {


        bool duplicated = false;
        for (int i = 0; i < sounds.Length; i++)
        {
            
            if (sounds[i].name == _name)
            {   
                if (duplicated)
                {
                    Debug.LogError("Error: same sound name exists");
                    return;
                }

                duplicated = !duplicated;
                sounds[i].Play();
                

            }
                
                    
                    
        }

    }
   

    private void Sinit()
    {
        GameObject sparent = new GameObject("Sparent");
        DontDestroyOnLoad(sparent);

        for (int i = 0; i < instance.sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            _go.transform.SetParent(sparent.transform);
            DontDestroyOnLoad(_go);

        }


    }


}
