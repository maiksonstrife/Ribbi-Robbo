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
}
