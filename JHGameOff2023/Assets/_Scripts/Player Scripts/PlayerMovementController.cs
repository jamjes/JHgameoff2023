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
    private float _jumpForce = 18.25f;
    private float _maxFallVelocity = -25;
    private float _jumpGravityScale = 4;
    private float _fallGravityScale = 5;
    private float _direction = 0;
    private Player _playerRef;
    public int DebugMoveMode = 0;

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
        if (Input.GetButtonDown("DebugRunnerStopMove"))
        {
            _canMove = !_canMove;
        }

        if (_canMove)
        {
            if (DebugMoveMode == 1) _direction = Input.GetAxis("RunnerHorizontal");
        }

        if (Input.GetButtonDown("RunnerJump") && _playerRef.CollisionHandler.IsGrounded())
        {
            Jump();
        }
        
        if (Input.GetButtonUp("RunnerJump") &&  _rb2d.velocity.y > 1)
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
    }

    private void FixedUpdate()
    {
        if (_canMove) _rb2d.velocity = new Vector2(_groundSpeed * _direction, _rb2d.velocity.y);

        if (_rb2d.velocity.y < _maxFallVelocity)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _maxFallVelocity);
        }
    }

    private void Jump()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
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
