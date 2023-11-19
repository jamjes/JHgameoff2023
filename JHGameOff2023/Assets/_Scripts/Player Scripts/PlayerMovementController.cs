using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private bool _canMove = true;
    [SerializeField] private float _groundSpeed = 6;
    [SerializeField] private float _wallRunSpeed = 10;
    [SerializeField] private float _jumpForce = 16f;
    [SerializeField] private float _reboundJumpForce = 12f;
    [SerializeField] private float _reboundMoveSpeed = 0;

    private float _maxFallVelocity = -25;
    private float _maxSlideVelocity = -5;

    private float _jumpGravityScale = 4;
    private float _fallGravityScale = 5;

    private float _direction = 0;
    private Player _playerRef;
    public int DebugMoveMode = 0;
    public bool _wallSliding = false;
    public bool _wallRunning = false;
    public bool _isWallJumping = false;

    private void OnEnable()
    {
        KillzoneEventManager.OnDeathEnter += DisableMovement;
    }

    private void OnDisable()
    {
        KillzoneEventManager.OnDeathEnter -= DisableMovement;
    }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerRef = GetComponent<Player>();
        if (DebugMoveMode == 0) _direction = 1;
    }

    private void Update()
    {
        bool grounded = _playerRef.CollisionHandler.IsGrounded();
        bool walled = _playerRef.CollisionHandler.IsWalled(_direction);

        AnimationUpdate(grounded, walled);

        if (!walled)
        {
            _wallSliding = false;
        }

        if (grounded)
        {
            if (_isWallJumping)
            {
                _isWallJumping = false;
            }
                
            if (_direction == -1)
            {
                _direction = 0;
                StartCoroutine(DelayedStart());
            }

        }

        if (_canMove)
        {
            if (DebugMoveMode == 1) _direction = Input.GetAxis("RunnerHorizontal");

            if (Input.GetButtonDown("RunnerJump") && grounded)
            {
                Jump();
            }

            if ((!grounded && walled) && _rb2d.velocity.y < 0)
            {
                if (!_wallSliding) _wallSliding = true;

                if (Input.GetAxis("RunnerVertical") == 1)
                {
                    _wallRunning = true;
                    UpdateGravityScale(0);
                }
            }

            if (Input.GetButtonDown("RunnerJump") && (!grounded && walled))
            {
                WallJump();
            }

            if (Input.GetButtonUp("RunnerJump") && _rb2d.velocity.y > 1)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, 1);
            }
            else if (Input.GetButtonUp("RunnerJump") && (_rb2d.velocity.y > 0 && _rb2d.velocity.y < 1))
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0);
            }

            if (_rb2d.velocity.y > 0 && _rb2d.gravityScale != _jumpGravityScale)
            {
                UpdateGravityScale(_jumpGravityScale);
            }
            else if (_rb2d.velocity.y < 0 && _rb2d.gravityScale != _fallGravityScale)
            {
                UpdateGravityScale(_fallGravityScale);
            }

            if (_wallRunning && !walled)
            {
                _wallRunning = false;
                UpdateGravityScale(_jumpGravityScale);
            }
        }
    }

    void AnimationUpdate(bool isGrounded, bool isWalled)
    {
        if (isGrounded)
        {
            GetComponent<PlayerAnimationHandler>().SetAnim("run");
        }
        else
        {
            if (_isWallJumping) //isWalled && Input.GetButtonDown("RunnerJump")
            {
                GetComponent<PlayerAnimationHandler>().SetAnim("backflip");
            }
            else if (_rb2d.velocity.y != 0)
            {
                GetComponent<PlayerAnimationHandler>().SetAnim("airbourne");
            }

        }
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            float yMaxVel;
            float xSpeed;

            if (_wallSliding)
            {
                yMaxVel = _maxSlideVelocity;
            }
            else
            {
                yMaxVel = _maxFallVelocity;
            }

            if (_isWallJumping)
            {
                xSpeed = _reboundMoveSpeed;
            }
            else
            {
                xSpeed = _groundSpeed;
            }
            _rb2d.velocity = new Vector2(xSpeed * _direction, Mathf.Clamp(_rb2d.velocity.y, yMaxVel, _jumpForce));
        }

        if (_wallRunning)
        {
            _rb2d.velocity = new Vector2(0, _wallRunSpeed);
        }
    }

    private void Jump()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
    }

    private void WallJump()
    {
        _direction *= -1;
        _isWallJumping = true;
        _rb2d.velocity = new Vector2(0, _reboundJumpForce);
    }

    private void DisableMovement()
    {
        _canMove = false;
        UpdateGravityScale(0);
        _rb2d.velocity = Vector2.zero;
    }

    private void UpdateGravityScale(float newVal)
    {
        _rb2d.gravityScale = newVal;
    }

    private IEnumerator DelayedStart()
    {
        DisableMovement();
        yield return new WaitForSeconds(.2f);
        _direction = 1;
        UpdateGravityScale(_fallGravityScale);
        _canMove = true;
    }
}
