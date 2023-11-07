using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour, IScoreSystem
{
    public Rigidbody2D Rb;
    public float JumpVelocity;
    public PlayerHUD Hud;
    public int Direction = 1;
    public int Mana;

    //public float DefaultGravityScale = 3;
    //public float FallingGravityScale = 5;
    public float FallClamp = -18;

    public Transform Feet;

    public float JumpButtonHeldTimeMax = .3f;
    public float JumpTimeRef = 0;
    public bool isJumping = false;
    public float Speed;

    public CollisionHandler col;

    private void Start()
    {
        //Rb.gravityScale = DefaultGravityScale;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Direction = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Direction = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && col.IsGrounded())
        {
            isJumping = true;
        }

        /*if (Rb.velocity.y >= 0 && Rb.gravityScale != DefaultGravityScale)
        {
            Rb.gravityScale = DefaultGravityScale;
        }
        else if (Rb.velocity.y < 0 && Rb.gravityScale != FallingGravityScale)
        {
            Rb.gravityScale = FallingGravityScale;
        }*/

        if (Rb.velocity.y < FallClamp)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, FallClamp);
        }

        if (isJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpVelocity);
            JumpTimeRef += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || JumpTimeRef > JumpButtonHeldTimeMax)
        {
            isJumping = false;
            JumpTimeRef = 0;
        }
    }

    private void FixedUpdate()
    {
        Rb.velocity = new Vector2(Speed * Direction, Rb.velocity.y);
    }

    public void Increment(int amount)
    {
        Mana += amount;
        Hud.UpdateManaValue(Mana);
    }
}
