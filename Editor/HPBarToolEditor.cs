using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HPBar))]
public class HPBarToolEditor : Editor
{

    /// <summary>
    /// Inspector�� ����ɶ� ȣ��� �Լ�, OnGUI�� ����ϴ�.
    /// </summary>
    public override void OnInspectorGUI()
    {
        //���� target���� ����ִ� ��ũ��Ʈ�� ������Ƽ���� ����Ǹ� �̿� ���� editor���� �����ϴµ��� ������Ʈ �޼ҵ带 ������
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
