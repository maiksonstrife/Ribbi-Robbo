using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRobotData", menuName = "Data/Robot Data/Base Robot Data")]
public class RobotData : ScriptableObject
{
    [Header("Global")]
    [SerializeField, Range(0f, 100f)]
    public float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    public float maxAcceleration = 10f;

    [SerializeField, Range(0f, 1f)]
    public float bounciness = 0.5f;

    [SerializeField]
    public Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
}
