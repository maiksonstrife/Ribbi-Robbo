using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody RB { get; private set; }
    public float maxDirectionResponsiveness { get; private set; }

    Vector3 velocity, desiredVelocity;

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody>();
    }

    public void LogicUpdate()
    {
        velocity = RB.velocity;
    }

    public void CharacterMovement()
    {
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxDirectionResponsiveness);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxDirectionResponsiveness);

        RB.velocity = velocity;
    }

    /// <summary>
    /// This Function Controls how much lag until DesiredVelocity reaches final Velocity
    /// </summary>
    public void SetMaxAcceleration(float maxDirectionResponsiveness)
    {
        this.maxDirectionResponsiveness = maxDirectionResponsiveness;
    }

    /// <summary>
    /// This Function ignores MaxAcceleration will immediately reach applied Velocity
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocity(Vector3 velocity)
    {
        RB.velocity = velocity;
        this.velocity = Vector3.zero;
    }

    /// <summary>
    /// This Function will make the velocity lags towards MaxAceleration that will be applied by the time the next frame occurs
    /// </summary>
    /// <param name="desiredVelocity"></param>
    public void SetDesiredVelocity(Vector3 desiredVelocity) => this.desiredVelocity = desiredVelocity;
}
