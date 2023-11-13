using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private bool _canMove = true;
    private float _groundSpeed = 6;
    private float _wallRunSpeed = 10;
    private float _jumpForce = 16f;
    private float _reboundJumpForce = 12f;
    
    
    private float _maxFallVelocity = -25;
    private float _maxSlideVelocity = -5;

    private float _jumpGravityScale = 4;
    private float _fallGravityScale = 5;

    private float _direction = 0;
    private Player _playerRef;
    public int DebugMoveMode = 0;
    public bool _wallSliding = false;
    public bool _wallRunning = false;

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


        if (!walled)
        {
            _wallSliding = false;
        }
        
        if (grounded)
        {
            if (_direction != 1) _direction = 1;
        }
        
        //if (Input.GetButtonDown("DebugRunnerStopMove"))
        //{
            //_canMove = !_canMove;
        //}

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
                //Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            float yMaxVel;

            if (_wallSliding)
            {
                yMaxVel = _maxSlideVelocity;
            }
            else
            {
                yMaxVel = _maxFallVelocity;
            }
            _rb2d.velocity = new Vector2(_groundSpeed * _direction, Mathf.Clamp(_rb2d.velocity.y, yMaxVel, _jumpForce));
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
}
