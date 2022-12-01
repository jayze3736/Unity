using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HPBar))]
public class HPBarToolEditor : Editor
{

    /// <summary>
    /// Inspector가 실행될때 호출될 함수, OnGUI와 비슷하다.
    /// </summary>
    public override void OnInspectorGUI()
    {
        //현재 target으로 삼고있는 스크립트의 프로퍼티들이 변경되면 이에 따른 editor값을 변경하는등의 업데이트 메소드를 실행함
        base.OnInspectorGUI();
        
        HPBar hPBar = (HPBar)target;

        if (GUILayout.Button("IncreaseHP")){
            hPBar.OnIncreaseHP();
        }

        if (GUILayout.Button("DecreaseHP"))
        {
            hPBar.OnDecreaseHP();
        }





    }


}
