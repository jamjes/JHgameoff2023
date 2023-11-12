using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public float ExtraHeight = 1f;
    public BoxCollider2D Bc;
    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    public bool WallJump;
    public int direction = 1;

    
    
    public bool IsGrounded()
    {
        bool grounded;
        RaycastHit2D raycastHit = Physics2D.BoxCast(Bc.bounds.center, Bc.bounds.size, 0f, Vector2.down, ExtraHeight, GroundLayer);
        grounded = raycastHit.collider != null;
        return grounded;
    }

    public bool CanWallJump()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(Bc.bounds.center, Bc.bounds.size, 0f, Vector2.right * direction, ExtraHeight, WallLayer);
        WallJump = raycastHit.collider != null;
        if (WallJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        return WallJump;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mana")
        {
            GetComponent<PlayerController>().Increment(1);
            Destroy(collision.gameObject);
        }
    }
}
