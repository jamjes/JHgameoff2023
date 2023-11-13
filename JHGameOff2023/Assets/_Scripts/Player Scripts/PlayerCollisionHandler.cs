using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private float ExtraHeight = .3f;
    public BoxCollider2D Bc;
    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    public bool WallJump;
    public int direction = 1;

    public bool IsGrounded()
    {

        RaycastHit2D hit = Physics2D.BoxCast(Bc.bounds.center, Bc.bounds.size, 0f, Vector2.down, ExtraHeight, GroundLayer);
        //DisplayGroundRays(hit.collider != null ? true : false);
        return hit.collider != null;
    }

    /*public bool CanWallJump()
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
    }*/

    private void DisplayGroundRays(bool condition)
    {
        Color rayColor;

        if (condition)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(Bc.bounds.center + new Vector3(Bc.bounds.extents.x, 0), Vector2.down * (Bc.bounds.extents.y + ExtraHeight), rayColor);
        Debug.DrawRay(Bc.bounds.center - new Vector3(Bc.bounds.extents.x, 0), Vector2.down * (Bc.bounds.extents.y + ExtraHeight), rayColor);
        Debug.DrawRay(Bc.bounds.center - new Vector3(Bc.bounds.extents.x, Bc.bounds.extents.y + ExtraHeight), Vector2.right * (Bc.bounds.extents.x * 2), rayColor);
    }
}
