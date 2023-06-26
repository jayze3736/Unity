using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    private void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("StatusIndicator: No healBarRect referenced ");
        }
        if (healthText == null)
        {
            Debug.LogError("StatusIndicator: No healthText referenced ");
        }
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;
        //1.0일때가 max, 0.5일때가 반피. 즉, 현재 체력/풀피 값으로 현재 체력이 얼만큼 남았는지 비율로 구하고
        //그 값을 scale로 집어넣으면 남아있는 피로 구현이 가능
        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = "HP" + _cur + "/" + _max;

    }

}
