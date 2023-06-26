using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusHP : MonoBehaviour
{
    // �׽�Ʈ

    float MaxHP;
    KnightPlayer player;

    [SerializeField]
    Transform HPGauge;

    float shakeMagnitudeOnDamage;
    float shakeDurationOnDamage;

    // StatusUI���� target�� �����;���
    // ������ Knight Player�� HP ���� ������ UI ���� 
    private void Start()
    {
        player = StatusUI.UI.Target;
        MaxHP = player.MaxHP;
        shakeDurationOnDamage = StatusUI.UI.FxData.HPbarDurationOnDamage;
        shakeMagnitudeOnDamage = StatusUI.UI.FxData.HPbarMagnintudeOnDamage;
        player.onDamage += OnDamage;
        // ���� OnDamage
    }

    // Update is called once per frame
    void Update()
    {
        float hp = player.HP;
        float ratio = hp / MaxHP;

        // ratio -> localscale
        // ����ó��: ratio�� 0~1 �������� ����� ���
        HPGauge.localScale = new Vector3(Mathf.Clamp(ratio, 0.0f, 1.0f), HPGauge.localScale.y, HPGauge.localScale.z);
        
    }

    public void OnDamage()
    {
        var animUtils = new AnimationUtils();
        StartCoroutine(animUtils.Shake(this.transform, shakeMagnitudeOnDamage, shakeDurationOnDamage));
    }
}
