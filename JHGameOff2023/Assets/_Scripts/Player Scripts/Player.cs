using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    //Player State Machine
    public enum PlayerState
    {
        Idle,
        Landing,
        Running,
        Airbourne,
        WallSliding,
        WallRunning,
        WallJumping,
        WallWaiting
    };

    public PlayerState CurrentState { private set; get; }
    public PlayerCollisionHandler Collision { private set; get; }
    public PlayerMovementController Movement { private set; get; }
    public PlayerAnimationHandler Animation { private set; get; }

    public ParticleSystem LandParticles;

    public bool IsDead;

    private void Start()
    {
        Collision = GetComponent<PlayerCollisionHandler>();
        Movement = GetComponent<PlayerMovementController>();
        Animation = transform.GetChild(0).GetComponent<PlayerAnimationHandler>();
    }

    private void Update()
    {
        if (Movement.qte)
        {
            Debug.Log("QTE!!");
            SetState(PlayerState.WallSliding);
        }
        else
        {
            if (Collision.IsGrounded())
            {
                if (Movement.Rb2d.velocity.x == 0 && !Movement.IsWaiting)
                {
                    SetState(PlayerState.Idle);
                }
                else if (Movement.Rb2d.velocity.x == 0 && Movement.IsWaiting)
                {
                    SetState(PlayerState.Landing);
                }
                else if (Movement.Rb2d.velocity.x != 0)
                {
                    SetState(PlayerState.Running);
                }
            }
            else if (!Collision.IsGrounded())
            {
                if (Collision.IsWalled(Movement.Direction))
                {
                    if (Movement._wallSliding && !Movement.IsWaiting)
                    {
                        SetState(PlayerState.WallSliding);
                    }
                    else if (Movement._wallRunning)
                    {
                        SetState(PlayerState.WallRunning);
                    }
                    else if (Movement.Rb2d.velocity.y > 0)
                    {
                        SetState(PlayerState.Airbourne);
                    }
                    else if (Movement.IsWaiting)
                    {
                        SetState(PlayerState.WallWaiting);
                    }
                }
                else
                {
                    if (Movement._isWallJumping)
                    {
                        SetState(PlayerState.WallJumping);
                    }
                    else if (!Movement._isWallJumping)
                    {
                        SetState(PlayerState.Airbourne);
                    }
                }
            }
        }
    }

    private void SetState(PlayerState newState)
    {
        CurrentState = newState;
        OnStateEnter(CurrentState);
    }

    private void OnStateEnter(PlayerState currentState)
    {
        Animation.PlayAnim(currentState);
    }
}
