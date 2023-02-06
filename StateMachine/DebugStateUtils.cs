using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStateUtils
{
    public static void DebugStateChange(string start, string end)
    {
        Debug.Log("EVENT: " + start + "->" + end);
    }

   

  

}
