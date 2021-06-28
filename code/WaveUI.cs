using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    /* State가 Counting될때 WaveCountdown 애니메이션 연출
     * : Waiting -> Counting이 될때로 제어할때와 State == Counting로 제어할때의 차이
     * % 만약에 후자(State == Counting)으로 제어할 경우, WaveIncoming 애니메이션을 연출할때 동시에 참이
     * 될 수 있기때문에 모호하므로 적절치 못함 따라서 전자로 제어
     * 
     * State가 Counting -> Spawning이 될때 WaveIncoming 애니메이션 연출     * 
     * 
     * //WaveCountdown
        /*if (spawner.State == WaveSpawner.SpawnState.COUNTING)
         *  if (spawner.State != WaveSpawner.SpawnState.SPAWNING)
         * 이렇게 코딩하는 이유는, Counting을 들어가기 시작해서, SPAWNING까지 WaveCountDown 함수를 실행하도록
         * 하기 위함이다.
         */
     



    //WaveSpawner의 WaveCountdown을 사용
    //WaveSpawner의 nextWave로 출몰하는 wave값을 UI에 표시
    [SerializeField]
    private WaveSpawner spawner;

    

    //WaveCountdown을 읽어서 그 값으로 Count Text를 바꿔야되므로
    [SerializeField]
    private Text wavecountdown;

    //nextWave값을 읽어서 Wave number UI에 표시
    [SerializeField]
    private Text wavecount;

    //WaveUIanimator의 변수값을 변경해야하므로
    [SerializeField]
    private Animator waveUIanimator;


    // Start is called before the first frame update
    void Start()
    {
        if(spawner == null)
        {
            Debug.LogError("Error: no spawner referenced");
        }

        else if (wavecountdown == null)
        {
            Debug.LogError("Error: no wavecountdown referenced");
        }

        else if (wavecount == null)
        {
            Debug.LogError("Error: no wavecount referenced");
        }

        else if (waveUIanimator == null)
        {
            Debug.LogError("Error: no waveUIanimator referenced");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //WaveIncoming
        if(spawner.State == WaveSpawner.SpawnState.SPAWNING)
        {
            if(spawner.State != WaveSpawner.SpawnState.WAITING)
            {
                WaveIncoming();
            }
        }

        //WaveCountdown
        /*if (spawner.State == WaveSpawner.SpawnState.COUNTING)
         *  if (spawner.State != WaveSpawner.SpawnState.SPAWNING)
         * 이렇게 코딩하는 이유는, Counting을 들어가기 시작해서, SPAWNING까지 WaveCountDown 함수를 실행하도록
         * 하기 위함이다.
         */
        if (spawner.State == WaveSpawner.SpawnState.COUNTING)
        {
            if (spawner.State != WaveSpawner.SpawnState.SPAWNING)
            {
                WaveCountdown();
            }
        }


    }

    private void WaveCountdown()
    {
        wavecountdown.text = ((int)spawner.WaveCountdown).ToString();
        waveUIanimator.SetBool("WaveIncoming", false);
        waveUIanimator.SetBool("WaveCount", true);
    }

    private void WaveIncoming()
    {
        wavecount.text = (spawner.NextWave).ToString();
        waveUIanimator.SetBool("WaveIncoming", true);
        waveUIanimator.SetBool("WaveCount", false);

    }
}
