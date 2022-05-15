using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Controller playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        playerController.CheckIfShouldFlip(xInput);
        
        playerController.SetVelocityX(playerData.movementVelocity * xInput);
        
        if (xInput == 0)
        {
            stateMachine.ChangeState(playerController.IdleState);
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
    }
}
