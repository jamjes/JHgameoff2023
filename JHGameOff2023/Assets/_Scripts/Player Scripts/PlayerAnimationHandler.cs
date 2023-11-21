using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int WallRun = Animator.StringToHash("wallrun");
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Airbourne = Animator.StringToHash("airbourne");
    private static readonly int Backflip = Animator.StringToHash("backflip");
    private static readonly int Wait = Animator.StringToHash("wait");
    private static readonly int WallWait = Animator.StringToHash("wallwait");
    private static readonly int Wallslide = Animator.StringToHash("wallslide");
    private Animator _anim;

    [SerializeField] private PlayerMovementController _playerRef;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.CrossFade(Run,0,0);
    }

    private void Update()
    {
        if (_playerRef._isGrounded)
        {
            if (_playerRef._isWaiting)
            {
                _anim.CrossFade(Wait, 0, 0);
            }
            else if (_playerRef._rb2d.velocity.x == 0)
            {
                _anim.CrossFade(Idle, 0, 0);
            }
            else
            {
                _anim.CrossFade(Run, 0, 0);
            }
        }
        else
        {
            if (_playerRef._isWaiting)
            {
                _anim.CrossFade(WallWait, 0, 0);
            }
            else if (_playerRef._wallRunning)
            {
                _anim.CrossFade(WallRun, 0, 0);
            }
            else if (_playerRef._wallSliding)
            {
                _anim.CrossFade(Wallslide, 0, 0);
            }
            else if (_playerRef._isWallJumping)
            {
                _anim.CrossFade(Backflip, 0, 0);
            }
            else
            {
                _anim.CrossFade(Airbourne, 0, 0);
            }
        }
    }
}
