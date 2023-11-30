using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D Rb2d { private set; get; }
    private bool _canMove = true;
    [SerializeField] private float _groundSpeed = 8;
    [SerializeField] private float _wallRunSpeed = 12;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private float _reboundJumpForce = 15f;
    [SerializeField] private float _reboundMoveSpeed = 12f;

    private float _maxFallVelocity = -25;
    private float _maxSlideVelocity = -5;

    private float _jumpGravityScale = 4;
    private float _fallGravityScale = 5;

    public float Direction = 1;
    private Player _playerRef;
    public bool _wallSliding = false;
    public bool _wallRunning = false;
    public bool _isWallJumping = false;
    public bool _isGrounded = false;
    public bool IsWaiting = false;
    public bool qte = false;
    public bool shrink = false;

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
        Rb2d = GetComponent<Rigidbody2D>();
        _playerRef = GetComponent<Player>();
    }

    private void Update()
    {
        bool grounded = _playerRef.Collision.IsGrounded();
        _isGrounded = grounded;
        bool walled = _playerRef.Collision.IsWalled(Direction);

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

                if (Direction == -1)
                {
                    Direction = 0;
                }

                IsWaiting = true;
                StartCoroutine(DelayedStart());
            }
                
            

        }

        if (_canMove)
        {
            if (Input.GetButtonDown("RunnerJump") && grounded)
            {
                Jump();
            }

            if ((!grounded && walled) && Rb2d.velocity.y < 0)
            {
                if (!_wallSliding) _wallSliding = true;

                if (Input.GetAxis("RunnerVertical") == 1)
                {
                    DisableMovement();
                    IsWaiting = true;
                    StartCoroutine(DelayedWallRun());
                }
            }

            if (Input.GetButtonDown("RunnerJump") && (!grounded && walled))
            {
                WallJump();
            }

            if (Input.GetButtonUp("RunnerJump") && Rb2d.velocity.y > 1)
            {
                Rb2d.velocity = new Vector2(Rb2d.velocity.x, 1);
            }
            else if (Input.GetButtonUp("RunnerJump") && (Rb2d.velocity.y > 0 && Rb2d.velocity.y < 1))
            {
                Rb2d.velocity = new Vector2(Rb2d.velocity.x, 0);
            }

            if (!shrink)
            {
                if (Rb2d.velocity.y > 0 && Rb2d.gravityScale != _jumpGravityScale)
                {
                    UpdateGravityScale(_jumpGravityScale);
                }
                else if (Rb2d.velocity.y < 0 && Rb2d.gravityScale != _fallGravityScale)
                {
                    UpdateGravityScale(_fallGravityScale);
                }
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

            Rb2d.velocity = new Vector2(xSpeed * Direction, Mathf.Clamp(Rb2d.velocity.y, yMaxVel, _jumpForce));
        }

        if (_wallRunning)
        {
            Rb2d.velocity = new Vector2(0, _wallRunSpeed);
        }
    }

    private void Jump()
    {
        Rb2d.velocity = new Vector2(Rb2d.velocity.x, _jumpForce);
    }

    private void WallJump()
    {
        Direction *= -1;
        _isWallJumping = true;
        Rb2d.velocity = new Vector2(_groundSpeed * Direction, _reboundJumpForce);
    }

    public void DisableMovement()
    {
        _canMove = false;
        UpdateGravityScale(0);
        Rb2d.velocity = Vector2.zero;
    }

    private void UpdateGravityScale(float newVal)
    {
        Rb2d.gravityScale = newVal;
    }

    private IEnumerator DelayedStart()
    {
        DisableMovement();
        yield return new WaitForSeconds(.4f);
        Direction = 1;
        UpdateGravityScale(_fallGravityScale);
        _canMove = true;
        IsWaiting = false;
    }

    private IEnumerator DelayedWallRun() 
    {
        yield return new WaitForSeconds(.4f);
        IsWaiting = false;
        _canMove = true;
        _wallSliding = false;
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
        Debug.Log("QTE Win");
        qte = false;
        Direction = 1;
        _canMove = true;
        shrink = true;
        Jump();
    }

    private void QTELoss()
    {
        qte = false;
        Direction = 1;
        _canMove = true;
        UpdateGravityScale(_jumpGravityScale);
        WallJump();
    }

    public void SetPhysicsMode(int mode)
    {
        if (mode == 0)
        {
            UpdateGravityScale(6);
            _groundSpeed = 6;
            _jumpForce = 15f;
            _wallRunSpeed = 10;
        }
        else
        {
            shrink = false;
            _groundSpeed = 10;
            _jumpForce = 20f;
            _wallRunSpeed = 15;
        }
    }

    public void UpdateMovementSettings(float groundRunSpeed, float groundJumpForce, float wallRunSpeed, float wallJumpForce, float wallReboundSpeed)
    {
        _groundSpeed = groundRunSpeed;
        _jumpForce = groundJumpForce;
        _wallRunSpeed = wallRunSpeed;
        _reboundJumpForce = wallJumpForce;
        _reboundMoveSpeed = wallReboundSpeed;
    }
}
