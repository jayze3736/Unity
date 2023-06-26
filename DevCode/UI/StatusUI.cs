using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    // ���� Player�� �������� ��� Pooling���� ó���Ѵٴ����� ������� UI�� ó���� �� ����
    // �ټ��� Player�� �ϳ��� ��� ����ó�� ���� -> ���� ���� Status ������ ���� View�� ó��

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
