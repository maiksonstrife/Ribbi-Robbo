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

    private string _previousState;
    private bool _canLog;

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
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
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

#if UNITY_EDITOR
    void OnGUI()
    {
        string parentState = StateMachine.CurrentState.ParentToString();
        GUI.Button(new Rect(0, 0, 200, 25), parentState);

        string currentState = StateMachine.CurrentState.ToString();
        GUI.Box(new Rect(205, 0, 200, 25), currentState);

        if (GUI.Button(new Rect(0, Screen.height - 25, 200, 25), $"Create States Log  {_canLog}")) _canLog = !_canLog;

        if (_previousState != currentState && _canLog) Debug.Log(currentState);

        _previousState = currentState;
    }
#endif
}
