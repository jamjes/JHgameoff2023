using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public float ExtraHeight = 1f;
    public BoxCollider2D Bc;
    public LayerMask GroundLayer;
    public bool grounded = false;

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(Bc.bounds.center, Bc.bounds.size, 0f, Vector2.down, ExtraHeight, GroundLayer);
        grounded = raycastHit.collider != null;
        return grounded;
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
