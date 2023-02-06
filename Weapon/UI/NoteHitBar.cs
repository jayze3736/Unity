using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitBar : MonoBehaviour
{
    public Transform createPoint;

    private void Start()
    {
        
    }

    void Update()
    {
        transform.position = createPoint.position;


    }




}
