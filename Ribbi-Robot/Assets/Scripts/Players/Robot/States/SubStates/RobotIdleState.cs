using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIdleState : RobotGroundedState
{
    public RobotIdleState(Robot robot, RobotStateMachine stateMachine, RobotData playerData, string animBoolName) : base(robot, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        robot.SetVelocity(Vector3.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (robot.Input.NormalizedInput.x != 0)
            {
                stateMachine.ChangeState(robot.MoveState);
            }
            //else if (yInput == -1)
            //{
            //    stateMachine.ChangeState(player.CrouchIdleState);
            //}
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
