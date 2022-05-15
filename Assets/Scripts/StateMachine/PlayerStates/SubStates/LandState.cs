using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : GroundedState
{
    public LandState(Controller playerController, StateMachine stateMachine, PlayerData playerData, string animationBool) : base(playerController, stateMachine, playerData, animationBool)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0)
        {
            stateMachine.ChangeState(playerController.moveState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(playerController.IdleState);
        }
    }
}
