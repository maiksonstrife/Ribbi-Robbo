using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public RobotStateMachine StateMachine { get; private set; }
    public RobotIdleState IdleState { get; private set; }
    public RobotMoveState MoveState { get; private set; }

    [SerializeField]
    private RobotData robotData;
    public RobotInput Input { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody RB { get; private set; }

    public Core Core { get; private set; }

    Vector3 velocity, desiredVelocity;

    private Vector2 workspace;
    private string _previousState;
    private bool _canLog;

    private void Awake()
    {
        StateMachine = new RobotStateMachine();
        IdleState = new RobotIdleState(this, StateMachine, robotData, "idle");
        MoveState = new RobotMoveState(this, StateMachine, robotData, "move");
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Input = GetComponent<RobotInput>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    /// <summary>
    /// This Function will immediately apply new Velocity
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

    public void Movement()
    {
        RB.velocity = velocity;

        float maxDirectionResponsiveness = robotData.maxAcceleration * Time.deltaTime;

        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxDirectionResponsiveness);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxDirectionResponsiveness);

        RB.velocity = velocity;
    }


#if UNITY_EDITOR
    void OnGUI()
    {
        string currentState = StateMachine.CurrentState.ToString();
        GUI.Box(new Rect(0, 0, 200, 25), currentState);

        if (GUI.Button(new Rect(0, Screen.height - 25, 200, 25), $"Create States Log        {_canLog}")) _canLog = !_canLog;

        if (_previousState != currentState && _canLog) Debug.Log(currentState);

        _previousState = currentState;
    }
#endif
}
