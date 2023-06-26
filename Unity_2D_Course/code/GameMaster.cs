using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]

public class GameMaster : MonoBehaviour
{

    //GameMaster Script는 몬스터를 스폰하거나, 아니면 일종의 플레이어 이동, 씬 변경 등을 관리하고 수행하도록 하는데 사용
    //Static function은 모든 script에서 공유됨
    
    //single tone pattern, 유일한 클래스 객체
    public static GameMaster gm;


    public float SpawnDelay = 2;
    public Transform PlayerPrefab;
    public Transform SpawnPoint;
    public Transform SpawnPrefab;
    public CameraShake camShake;

    public float shakeAmt = 0.1f;
    //흔들리는 시간
    public float length = 0.2f;

    /*
     * Player 스크립트에서 playerlives를 관리하려고 하는 경우, static이더라도 스크립트 초기화시,
       값이 초기화되므로 문제가 발생
    */
    
    public int playerlives = 3;

    [SerializeField]
    private Text playerlives_text;

    [SerializeField]
    private string RespawnMusic = "RespawnCountdown";

    [SerializeField]
    private string BackgroundMusic = "BackgroundMusic";

    [SerializeField]
    private string SpawnSound = "Respawn";

    private void Start()
    {
        if(gm == null)
        {
            //내가 보기엔 GameObject 클래스의 오브젝트 중에서 Tag가 GM인 오브젝트의 Component 성분인 GameMaster를 가져오라고 하는 뜻인듯
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        if(PlayerPrefab == null)
        {
            Debug.LogError("No PlayerPrefab referenced: GameMaster.cs");
        }
        else if (SpawnPoint == null)
        {
            Debug.LogError("No SpawnPoint referenced: GameMaster.cs");
        }
        else if(SpawnPrefab == null)
        {
            Debug.LogError("No SpawnPrefab referenced: GameMaster.cs");
        }
        else if (camShake == null)
        {
            Debug.LogError("No camShake referenced: GameMaster.cs");
        }
        else if(playerlives_text == null)
        {
            Debug.LogError("No playerlives_text referenced: GameMaster.cs");
        }
       

        gm.SetLivesUI();
        AudioManager.instance.PlaySound(BackgroundMusic);

            
    }


    //단순히 Player를 리스폰하는 함수
    public IEnumerator RespawnPlayer()
    {
        //Counting Spawn Delay
        AudioManager.instance.PlaySound(RespawnMusic);
        yield return new WaitForSeconds(SpawnDelay);

        //Spawn Player
        Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
        
        //Spawn Effect
        Transform clone = Instantiate(SpawnPrefab, SpawnPoint.position, SpawnPoint.rotation);
        Destroy(clone.gameObject, 3f);

        //Spawn Music
        AudioManager.instance.PlaySound(SpawnSound);
        



    }


    //static 함수이기때문에 외부에서 접근가능
    public static void KillPlayer(Player player)
    {
        --gm.playerlives;
        if (!(gm.playerlives <= 0))
        {
            Destroy(player.gameObject);
            gm.StartCoroutine(gm.RespawnPlayer());

        }
        gm.SetLivesUI();

    }

    public static void KillEnemy(Enemy enemy)
    {

        Destroy(enemy.gameObject);

    }


    public void SetLivesUI()
    {
        playerlives_text.text = "Remaining lives:" + gm.playerlives;

    }





}
