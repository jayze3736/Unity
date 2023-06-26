using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    /* 중첩 클래스 inner Class: 클래스 내에 클래스를 두어 멤버 변수와 메소드등을 편리하게 또는 알아보기 쉽게 수정및 관리하기위해서 사용
    Player의 경우에도 정보가 여러가지 있을 수 있는데, 예를 들면 플레이어의 스탯 클래스, 타 등장인물 또는 몬스터와의 상호작용 클래스, 스킬 클래스 등 Player의 정보도 내부 클래스를 통해
    관리가 가능하다.*/
    

    // 중첩 클래스 내에 public 멤버 변수가 있을때 사용
    // _cur: private, cur: public 변수
    [System.Serializable]
    public class PlayerStats
    {
        public int maxhealth = 100;

        private int _curhealth;
        public int curhealth
        {
            get { return _curhealth; }
            set { _curhealth = Mathf.Clamp(value, 0, maxhealth); }
        }

        public void Init()
        {
            curhealth = maxhealth;

        }

        
    }

    // 객체를 생성해야 클래스의 변수및 메소드 사용가능
    public PlayerStats playerstats = new PlayerStats();
    public int fallBoundary = -20;


    [SerializeField]
    private string DeathVoice = "DeathVoice";


    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    

    private void Start()
    {
        playerstats.Init();


        if (statusIndicator == null)
        {
            Debug.LogError("No statusIndicator referenced: Player.cs");
        }
        else
        {
            statusIndicator.SetHealth(playerstats.curhealth, playerstats.maxhealth);
            
        }


        
    }


    private void Update()
    {

        if (transform.position.y <= fallBoundary)
        {

            //very large integer number to make sure killing the player
            this.gameObject.SetActive(false);
            DamagePlayer(999999);
            

        }

    }

    


    public void DamagePlayer(int Damage)
    {
        playerstats.curhealth -= Damage;

        if(playerstats.curhealth <= 0)
        {
            
            GameMaster.KillPlayer(this);
            AudioManager.instance.PlaySound(DeathVoice);
            Debug.Log("GAME OVER");
        }

        if (statusIndicator == null)
        {
            Debug.LogError("No statusIndicator referenced: Player.cs");
        }
        else
        {
            statusIndicator.SetHealth(playerstats.curhealth, playerstats.maxhealth);
        }

        GameMaster.gm.camShake.Shake(GameMaster.gm.shakeAmt, GameMaster.gm.length);
    }

   

}
