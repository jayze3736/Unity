using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "KnightData/KnightMoveData", order = 1)]
public class KnightMoveData : ScriptableObject
{
    #region Properties - ReadOnly
    public float LerpAmount { get => lerpAmount; }
    public float RunAccelAmount { get => runAccelAmount; }
    public float RunDeccelAmount { get => runDeccelAmount; }
    public float FrictionAmount { get => frictionAmount; }
    public float JumpForce { get => jumpForce; }
    public float GravityScale { get => gravityScale; }
    public float FallGravityMultiplier { get => fallGravityMultiplier; }
    public float JumpCutMultiplier { get => jumpCutMultiplier; }
    public float CoyoteTime { get => coyoteTime; }
    #endregion

    #region Properties
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    #endregion





    #region Move Mode
    [SerializeField]
    float maxSpeed;

    [Range(0f, 1f)]
    [SerializeField]
    float lerpAmount;

    [SerializeField]
    float runAccelAmount;

    [SerializeField]
    float runDeccelAmount;

    [SerializeField]
    float frictionAmount;

    #endregion

    #region JUMP
    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravityScale;

    [SerializeField]
    float fallGravityMultiplier;

    [Range(0f, 1f)]
    [SerializeField]
     float jumpCutMultiplier;

    #endregion

    #region Timer
    [SerializeField]
     float coyoteTime;

    #endregion

    #region State Variable
    public bool isJumping;
    public bool isFalling;

    #endregion

   


}
