using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private BoxCollider2D _boxCol;
    private float _detectionLeniency = .3f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private bool DEBUG_MODE = false;

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider2D>();
    }

    public bool IsGrounded()
    {

        RaycastHit2D hit = Physics2D.BoxCast(_boxCol.bounds.center, _boxCol.bounds.size, 0f, Vector2.down, .01f, GroundLayer);
        if (DEBUG_MODE) DisplayGroundRays(hit.collider != null ? true : false);
        return hit.collider != null;
    }

    public bool IsWalled(float direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCol.bounds.center, _boxCol.bounds.size, 0f, Vector2.right * direction, .01f, WallLayer);
        return hit.collider != null;
    }

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

        Debug.DrawRay(_boxCol.bounds.center + new Vector3(_boxCol.bounds.extents.x, 0), Vector2.down * (_boxCol.bounds.extents.y + _detectionLeniency), rayColor);
        Debug.DrawRay(_boxCol.bounds.center - new Vector3(_boxCol.bounds.extents.x, 0), Vector2.down * (_boxCol.bounds.extents.y + _detectionLeniency), rayColor);
        Debug.DrawRay(_boxCol.bounds.center - new Vector3(_boxCol.bounds.extents.x, _boxCol.bounds.extents.y + _detectionLeniency), Vector2.right * (_boxCol.bounds.extents.x * 2), rayColor);
    }
}
