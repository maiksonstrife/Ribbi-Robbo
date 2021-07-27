using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGroundedState : RobotState
{
    protected Vector3 RobotInput;

    public RobotGroundedState(Robot robot, RobotStateMachine stateMachine, RobotData playerData, string animBoolName) : base(robot, stateMachine, playerData, animBoolName)
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
        RobotInput = new Vector3(robot.Input.NormalizedInput.x, 0f, robot.Input.NormalizedInput.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
