using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    /* 1. C#의 특징, 변수에 set & get을 선언하여 변수를 읽을 때(get)와 쓸 때(set)의 두가지 경우로 
     * 나누어 값을 반환하거나 쓸때 조건, 제약 등을 줄 수 있다. get을 설정할땐 return이 항상 필요하고
     * set을 할때는 value가 필요하다. 이때 value는 쓸 때 대입되는 값이다.
     * 또한 set과 get을 작성할때 결과값을 다른 변수로 부터 읽거나 그 변수에 쓰도록 하는 것이 가능하다.
     * 
     * ex)
     *  private int _curhealth;
     *  public int curhealth
        {
            get { return _curhealth; }
            set { _curhealth = Mathf.Clamp(value, 0,maxhealth); }
        }

     * 
     * 
     * 
     * 
     * 
     */


    [System.Serializable]

    public class EnemyStats
    {
        //underscore: access
        private int _curhealth;
        //Suggested way(_HardCode): Class Constructor(생성자)
        public int maxhealth = 100;
        public int curhealth
        {
            get { return _curhealth; }
            set { _curhealth = Mathf.Clamp(value, 0,maxhealth); }
        }

        public void Init()
        {
            curhealth = maxhealth;
        }
        


    }

    public EnemyStats enemystats = new EnemyStats();

    /*
     * Inspector에 Optional: 이라고 문구가 뜸, 일종의 주석처리를 할때 [Header(String)]을 사용
     */
    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        enemystats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemystats.curhealth, enemystats.maxhealth);
        }

    }


    public void DamageEnemy(int Damage)
    {
        enemystats.curhealth -= Damage;

        if(enemystats.curhealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }


        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemystats.curhealth, enemystats.maxhealth);
        }

        GameMaster.gm.camShake.Shake(GameMaster.gm.shakeAmt, GameMaster.gm.length);

    }




   
}
