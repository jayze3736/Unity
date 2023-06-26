using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatcherData : ScriptableObject
{
    public LayerMask PlayerLayer { get => playerLayer; }
    public float FindSightRange { get => xFindSightRange; }

    public float PlayerNearRange { get => xPlayerNearRange; }

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    float xFindSightRange;

    [SerializeField]
    float xPlayerNearRange;


}
