using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : PlayerStates
{
    protected bool isAbilityDone;
    private bool isGrounded;
    
    public AbilityState(Controller playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            //to not switch state on the first frame of the ablility
            if (isGrounded && playerController.currentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(playerController.IdleState);
            }
            else
            {
                stateMachine.ChangeState(playerController.inAirState);
            }
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
