using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonsterHPBar : MonoBehaviour
{
    [SerializeField]
    Transform HP;

    [SerializeField]
    Enemy enemy; //enemy의 OnDamaged 나 이벤트 리스너에 UpdateHPBar 가입시킴

    float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        enemy.onHPUpdate += UpdateHPBar;
        maxHp = enemy.MAXHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHPBar(float hp)
    {
        float ratio = Mathf.Clamp((hp / maxHp), 0f, 1f);
        HP.localScale = new Vector3(ratio, HP.localScale.y, HP.localScale.z);




    }


}
