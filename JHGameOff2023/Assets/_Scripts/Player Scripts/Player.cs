using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerCollisionHandler CollisionHandler { private set; get; }

    private void Start()
    {
        CollisionHandler = GetComponent<PlayerCollisionHandler>();
    }
}
