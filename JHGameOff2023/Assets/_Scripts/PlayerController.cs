using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour, IScoreSystem
{
    #region Variables

    [Header("Unity Components")]
    private Rigidbody2D _rigidBody2D; //Unity's Built-in Physics Manager

    [Header("Custom Components")]
    private PlayerHUD _playerHUD; //Display Player Attributes on Canvas
    private CollisionHandler _collisionHandler; //Custom collision events

    [Header("Player Attributes")]
    private int _mana;
    private int _direction = 1;
    [SerializeField] private float _speed = 6;
    [SerializeField] private float _jumpVelocity = 15;
    [SerializeField] private Transform _feet; //Reference to player feet position

    [Header("Player Physics")]
    [SerializeField] private float _maxFallVelocity = -18; //Maximum value applied to player negative y velocity
    [SerializeField] private float _jumpButtonHeldTimeMax = .3f; //Time jump button can be held to increase positive y velocity
    private float _jumpTimeRef = 0;
    private bool _isJumping = false;

    #endregion

    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _playerHUD = GetComponent<PlayerHUD>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _direction = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _direction = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _collisionHandler.IsGrounded()) //If Jump key is triggered and player is overlapping with Ground
        {
            _isJumping = true;
        }

        if (_rigidBody2D.velocity.y < _maxFallVelocity) //Applies when player negative y velocity is more than custom clamp value
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _maxFallVelocity); //Reset y velocity to custom clamp value
        }

        if (_isJumping)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _jumpVelocity);
            _jumpTimeRef += Time.deltaTime; //Run jump held timer
        }

        if (Input.GetKeyUp(KeyCode.Space) || _jumpTimeRef > _jumpButtonHeldTimeMax) //If player releases Jump button OR player has held Jump button for max time
        {
            _isJumping = false;
            _jumpTimeRef = 0; //Reset jump held timer
        }
    }

    private void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(_speed * _direction, _rigidBody2D.velocity.y);
    }

    #region Interface Function

    public void Increment(int amount)
    {
        _mana += amount;
        _playerHUD.UpdateManaValue(_mana);
    }

    #endregion
}
