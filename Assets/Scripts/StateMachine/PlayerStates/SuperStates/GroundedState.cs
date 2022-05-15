using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerStates
{

    protected int xInput;
    private bool jumpInput;
    private bool isGrounded;
    
    public GroundedState(Controller playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool) {}

    public override void Enter()
    {
        base.Enter();
        playerController.jumpState.ResetJump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = playerController.playerInputHandler.normalizedInputX;
        jumpInput = playerController.playerInputHandler.jumpInput;

        if (jumpInput && playerController.jumpState.CanJump())
        {
            playerController.playerInputHandler.UseJumpInput();
            stateMachine.ChangeState(playerController.jumpState);
        }
        else if (!isGrounded)
        {
            playerController.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(playerController.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = playerController.CheckIfGrounded();
    }
}
