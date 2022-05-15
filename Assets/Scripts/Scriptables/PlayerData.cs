using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Player")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity;

    [Header("Jump State")]
    public float jumpVelocity;
    public int amountOfJumps = 2;

    [Header("In Air State")] 
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMult = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.1f;

    public LayerMask whatIsGround;
}
