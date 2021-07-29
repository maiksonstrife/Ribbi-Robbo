using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMoveState : RobotGroundedState
{
    public RobotMoveState(Robot robot, RobotStateMachine stateMachine, RobotData playerData, string animBoolName) : base(robot, stateMachine, playerData, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetDesiredVelocity(RobotInput * robotData.maxSpeed);

        if (!isExitingState)
        {
            if (robot.Input.NormalizedInput == Vector2.zero)
            {
                stateMachine.ChangeState(robot.IdleState);
            }
            //else if (yInput == -1)
            //{
            //    stateMachine.ChangeState(player.CrouchMoveState);
            //}
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        robot.Core.Movement.CharacterMovement();
    }
}
