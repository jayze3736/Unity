using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    // 만약 Player가 여러명일 경우 Pooling으로 처리한다던가의 방법으로 UI를 처리할 수 있음
    // 다수의 Player를 하나로 묶어서 집합처럼 관리 -> 집합 내의 Status 로직을 전부 View에 처리

    public static StatusUI UI = null;


    [SerializeField]
    KnightPlayer target;

    [SerializeField]
    UI_FX_Data FX_Data;

    public KnightPlayer Target { get { return target; } }
    public UI_FX_Data FxData { get { return FX_Data; } }



    // Start is called before the first frame update
    void Awake()
    {
        if(UI == null)
        {
            UI = this;
        }
        else if(UI != this)
        {
            Destroy(UI.gameObject);
        }
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
