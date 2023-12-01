using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.position = new Vector2(transform.position.x + .01f, transform.position.y);
    }
}
