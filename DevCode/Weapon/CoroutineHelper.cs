using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper helper = null;

    private void Start()
    {
        if(helper != null)
        {
            Destroy(this.gameObject);
        }   
        else if(helper == null)
        {
            helper = this;
        }
    }

}
