using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotInput : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 RawMovementInput { get; private set; }
    public Vector2 NormalizedInput { get; private set; }

    public void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormalizedInput = RawMovementInput.normalized; //Preserva comprimento (caminha por toda area do circulo)
        //NormalizedInput = Vector2.ClampMagnitude(RawMovementInput, 1f); //Preserva Dire??o (caminha somente a extens?o do circulo)
    }
}
