using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private bool _move = true;
    [SerializeField] private float _groundSpeed = 6;
    [SerializeField] private float _jumpForce = 18.25f;
    [SerializeField] private float _maxFallVelocity = -25;
    private float _direction = 0;
    private Player _playerRef;
    public bool grounded = false;
    public int moveMode = 0;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerRef = GetComponent<Player>();
        if (moveMode == 0) _direction = 1;
    }

    private void Update()
    {
        //grounded = _playerRef.CollisionHandler.IsGrounded();

        Debug.Log(_rb2d.velocity.y);
        
        if (Input.GetButtonDown("DebugRunnerStopMove"))
        {
            _move = !_move;
        }

        if (_move)
        {
            if (moveMode == 1) _direction = Input.GetAxis("RunnerHorizontal");
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



        if (_rb2d.velocity.y > 0 && _rb2d.gravityScale != 4)
        {
            _rb2d.gravityScale = 4;
        }
        else if (_rb2d.velocity.y < 0 && _rb2d.gravityScale != 5)
        {
            _rb2d.gravityScale = 5;
        }
    }

    private void FixedUpdate()
    {
        if (_move) _rb2d.velocity = new Vector2(_groundSpeed * _direction, _rb2d.velocity.y);

        if (_rb2d.velocity.y < _maxFallVelocity)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _maxFallVelocity);
        }
    }

    private void Jump()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
    }
}
