using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunnerController))]
public class PlayerInputController : MonoBehaviour
{
    public delegate void PlayerInput();

    public static event PlayerInput OnJumpEnter;
    public static event PlayerInput OnPauseEnter;
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpEnter();
        }

        if (Input.GetButtonDown("Pause"))
        {
            OnPauseEnter();
        }
    }
}
