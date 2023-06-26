using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "UI_FX_Data", order = 1)]
public class UI_FX_Data : ScriptableObject
{
    #region HP bar
    [SerializeField]
    float HP_Bar_MagnitudeOnDamage;

    [SerializeField]
    float HP_Bar_DurationOnDamage;

    public float HPbarMagnintudeOnDamage { get => HP_Bar_MagnitudeOnDamage; }
    public float HPbarDurationOnDamage { get => HP_Bar_DurationOnDamage; }

    #endregion


}
