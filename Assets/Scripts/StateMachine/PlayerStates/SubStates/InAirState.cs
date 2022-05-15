using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerStates
{

    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool coyoteTime;
    private bool isJumping;
    private bool jumpInputStop;
    public InAirState(Controller playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        CheckCoyoteTime();

        xInput = playerController.playerInputHandler.normalizedInputX;
        jumpInput = playerController.playerInputHandler.jumpInput;
        jumpInputStop = playerController.playerInputHandler.jumpInputStop;

        CheckJumpMultiplier();

        if (isGrounded && playerController.currentVelocity.y < 0.01f)
        {
            playerController.animator.SetFloat("Yvel", 0f);
            stateMachine.ChangeState(playerController.landState);
        }
        else if(jumpInput && playerController.jumpState.CanJump())
        {
            stateMachine.ChangeState(playerController.jumpState);
        }
        else
        {
            playerController.CheckIfShouldFlip(xInput);
            playerController.SetVelocityX(playerData.movementVelocity * xInput);
            
            playerController.animator.SetFloat("Yvel", playerController.currentVelocity.y);
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

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            playerController.jumpState.DecreaseAmountOfJump();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void SetIsJumping() => isJumping = true;
    

    //the longer the player press, higher the jump will be
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                playerController.SetVelocityY(playerController.currentVelocity.y * playerData.variableJumpHeightMult);
                isJumping = false;
            }
            else if(playerController.currentVelocity.y <= 0.0f)
            {
                isJumping = false;
            }
        }
    }
}
