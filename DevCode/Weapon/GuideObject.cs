using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideObject : MonoBehaviour
{
    public bool bMoving = true;


    public Vector3 GetWorldPosition()
    {
        return this.transform.position;
    }

    public void SetWorldPosition(Vector3 worldPos)
    {
        transform.position = worldPos;
    }

    public void Enable()
    {
        Debug.Log("ENABLE: " + transform.name);
        this.transform.gameObject.SetActive(true);
    }

    public void Disable()
    {
        Debug.Log("DISABLE: " + transform.name);
        this.transform.gameObject.SetActive(false);
    }




}
