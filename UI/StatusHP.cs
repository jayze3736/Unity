using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusHP : MonoBehaviour
{
    // 테스트

    float MaxHP;
    KnightPlayer player;

    [SerializeField]
    Transform HPGauge;

    float shakeMagnitudeOnDamage;
    float shakeDurationOnDamage;

    // StatusUI에서 target을 가져와야함
    // 가져온 Knight Player의 HP 값을 가지고 UI 세팅 
    private void Start()
    {
        player = StatusUI.UI.Target;
        MaxHP = player.MaxHP;
        shakeDurationOnDamage = StatusUI.UI.FxData.HPbarDurationOnDamage;
        shakeMagnitudeOnDamage = StatusUI.UI.FxData.HPbarMagnintudeOnDamage;
        player.onDamage += OnDamage;
        // 가입 OnDamage
    }

    // Update is called once per frame
    void Update()
    {
        float hp = player.HP;
        float ratio = hp / MaxHP;

        // ratio -> localscale
        // 예외처리: ratio가 0~1 범위에서 벗어나는 경우
        HPGauge.localScale = new Vector3(Mathf.Clamp(ratio, 0.0f, 1.0f), HPGauge.localScale.y, HPGauge.localScale.z);
        
    }

    public void OnDamage()
    {
        var animUtils = new AnimationUtils();
        StartCoroutine(animUtils.Shake(this.transform, shakeMagnitudeOnDamage, shakeDurationOnDamage));
    }
}
