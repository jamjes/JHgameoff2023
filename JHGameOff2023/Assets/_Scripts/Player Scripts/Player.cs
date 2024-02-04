using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Experimental.GraphView;
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

    public bool IsDead;
    public GameSettings Settings;
    private bool started = false;
    public delegate void Level();
    public static event Level OnPauseEnter;

    public delegate void Audio();
    public static event Audio MuteSoundFX;
    public static event Audio UnmuteSoundFX;

    public AudioSource LandSound;
    public AudioSource BGMusic;

    private bool pauseStatus = false;

    private void OnEnable()
    {
        PlayerQTEController.OnQTEWin += PitchUpMusic;
        PlayerCollisionHandler.OnShrinkExit += RevertMusicPitch;
    }

    private void OnDisable()
    {
        PlayerQTEController.OnQTEWin -= PitchUpMusic;
        PlayerCollisionHandler.OnShrinkExit -= RevertMusicPitch;
    }

    void PitchUpMusic()
    {
        BGMusic.pitch = 1.3f;
    }

    void RevertMusicPitch()
    {
        BGMusic.pitch = 1f;
    }

    private void Start()
    {
        Cursor.visible = false;
        Collision = GetComponent<PlayerCollisionHandler>();
        Movement = GetComponent<PlayerMovementController>();
        Animation = transform.GetChild(0).GetComponent<PlayerAnimationHandler>();
        if (!Settings.SOUND_FX)
        {
            MuteSoundFX();
            LandSound.volume = 0;
        }
        else if (Settings.SOUND_FX)
        {
            UnmuteSoundFX();
            LandSound.volume = .4f;
        }

        if (!Settings.BG_MUSIC)
        {
            BGMusic.volume = 0;
        }
        else if (Settings.BG_MUSIC)
        {
            BGMusic.volume = .8f;
            StartCoroutine(DelayedBGMusic());
        }

        CurrentState = PlayerState.Idle;
        
    }

    IEnumerator DelayedBGMusic()
    {
        yield return new WaitForSeconds(3);
        BGMusic.Play();
    }

    

    private void Update()
    {
        //if (Input.GetButtonDown("Pause"))
        //{
            //if (OnPauseEnter != null)
            //{
                //OnPauseEnter();
                //Debug.Log("Pause Event Called");
            //}
        //}
        
        if (Input.GetButtonDown("Pause"))
        {
            if (pauseStatus == false)
            {
                //GameManager.instance.StopTimer();
                pauseStatus = true;
            }
            else
            {
                //GameManager.instance.StartTimer();
                pauseStatus = false;
            }
        }

        
        if (Movement.qte)
        {
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
                else if (Movement.Rb2d.velocity.x == 0 && (Movement.IsWaiting && CurrentState != PlayerState.Landing))
                {
                    SetState(PlayerState.Landing);
                    LandSound.Play();
                }
                else if (Movement.Rb2d.velocity.x != 0)
                {
                    if (Movement.CanMove) SetState(PlayerState.Running);
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
                    if (Movement._isWallJumping && CurrentState != PlayerState.WallJumping)
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
