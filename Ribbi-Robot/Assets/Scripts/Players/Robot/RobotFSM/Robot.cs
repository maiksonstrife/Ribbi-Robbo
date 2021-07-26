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

    public Core Core { get; private set; }

    Vector3 velocity;

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

    public void SetVelocity(Vector3 velocity) => this.velocity = velocity;

    public void Movement()
    {
        Vector3 desiredVelocity =
            new Vector3(Input.NormalizedInput.x, 0f, Input.NormalizedInput.y) * robotData.maxSpeed;

        float maxSpeedChange = robotData.maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;
        if (newPosition.x < robotData.allowedArea.xMin)
        {
            newPosition.x = robotData.allowedArea.xMin;
            velocity.x = -velocity.x * robotData.bounciness;
        }
        else if (newPosition.x > robotData.allowedArea.xMax)
        {
            newPosition.x = robotData.allowedArea.xMax;
            velocity.x = -velocity.x * robotData.bounciness;
        }
        if (newPosition.z < robotData.allowedArea.yMin)
        {
            newPosition.z = robotData.allowedArea.yMin;
            velocity.z = -velocity.z * robotData.bounciness;
        }
        else if (newPosition.z > robotData.allowedArea.yMax)
        {
            newPosition.z = robotData.allowedArea.yMax;
            velocity.z = -velocity.z * robotData.bounciness;
        }
        transform.localPosition = newPosition;
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
