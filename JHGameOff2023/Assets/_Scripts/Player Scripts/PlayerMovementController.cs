using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D _rb2d;
    private bool _canMove = true;
    /*[SerializeField]*/ private float _groundSpeed = 8;
    /*[SerializeField]*/ private float _wallRunSpeed = 12;
    /*[SerializeField]*/ private float _jumpForce = 20f;
    /*[SerializeField]*/ private float _reboundJumpForce = 12f;
    /*[SerializeField]*/ private float _reboundMoveSpeed = 8f;

    private float _maxFallVelocity = -25;
    private float _maxSlideVelocity = -5;

    private float _jumpGravityScale = 4;
    private float _fallGravityScale = 5;

    public float _direction = 1;
    private Player _playerRef;
    public bool _wallSliding = false;
    public bool _wallRunning = false;
    public bool _isWallJumping = false;
    public bool _isGrounded = false;
    public bool _isWaiting = false;
    public bool qte = false;

    private void OnEnable()
    {
        KillzoneEventManager.OnDeathEnter += DisableMovement;
        PlayerCollisionHandler.OnQTEEnter += BeginQTE;
        PlayerQTEController.OnQTEWin += QTEWin;
        PlayerQTEController.OnQTELoss += QTELoss;
    }

    private void OnDisable()
    {
        KillzoneEventManager.OnDeathEnter -= DisableMovement;
        PlayerCollisionHandler.OnQTEEnter -= BeginQTE;
        PlayerQTEController.OnQTEWin -= QTEWin;
        PlayerQTEController.OnQTELoss -= QTELoss;
    }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerRef = GetComponent<Player>();
    }

    private void Update()
    {
        bool grounded = _playerRef.CollisionHandler.IsGrounded();
        _isGrounded = grounded;
        bool walled = _playerRef.CollisionHandler.IsWalled(_direction);

        if (!walled)
        {
            _wallSliding = false;
        }

        if (grounded)
        {
            _wallSliding = false;

            if (_isWallJumping)
            {
                _isWallJumping = false;
            }
                
            if (_direction == -1)
            {
                _direction = 0;
                _isWaiting = true;
                StartCoroutine(DelayedStart());
            }

        }

        if (_canMove)
        {
            if (Input.GetButtonDown("RunnerJump") && grounded)
            {
                Jump();
            }

            if ((!grounded && walled) && _rb2d.velocity.y < 0)
            {
                if (!_wallSliding) _wallSliding = true;

                if (Input.GetAxis("RunnerVertical") == 1)
                {
                    DisableMovement();
                    _isWaiting = true;
                    StartCoroutine(DelayedWallRun());
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
            _rb2d.velocity = new Vector2(0, _wallRunSpeed * _direction);
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
        _rb2d.velocity = new Vector2(_groundSpeed * _direction, _reboundJumpForce);
    }

    public void DisableMovement()
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
        yield return new WaitForSeconds(.4f);
        _direction = 1;
        UpdateGravityScale(_fallGravityScale);
        _canMove = true;
        _isWaiting = false;
    }

    private IEnumerator DelayedWallRun() 
    {
        yield return new WaitForSeconds(.4f);
        _isWaiting = false;
        _canMove = true;
        _wallRunning = true;
    }

    private void BeginQTE()
    {
        DisableMovement();
        _wallRunning = false;
        qte = true;
    }

    private void QTEWin()
    {
        qte = false;
        _direction = 1;
        _canMove = true;
        UpdateGravityScale(_jumpGravityScale);
        Jump();
    }

    private void QTELoss()
    {
        qte = false;
        _direction = 1;
        _canMove = true;
        UpdateGravityScale(_jumpGravityScale);
        WallJump();
    }
}
