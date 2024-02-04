using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 3;
    public int direction = 1;
    public float jumpForce = 12;

    private void OnEnable()
    {
        PlayerInputController.OnJumpEnter += Jump;
    }

    private void OnDisable()
    {
        PlayerInputController.OnJumpEnter -= Jump;
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }
}
