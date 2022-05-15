using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbilityState
{
    private int amountOfJumpsLeft;
    public JumpState(Controller playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        playerController.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        playerController.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        return amountOfJumpsLeft > 0;
    }

    public void ResetJump() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJump() => amountOfJumpsLeft--;
}
