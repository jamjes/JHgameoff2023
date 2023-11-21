using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    public GameObject LookAt;
    public float xOffset = 8, yOffset;

    private void Start()
    {
        Vector3 camPos = new Vector3(LookAt.transform.position.x + xOffset, LookAt.transform.position.y + yOffset, -10);
        
        Camera.main.transform.position = camPos;
    }

    private void FixedUpdate()
    {
        float xPos = LookAt.transform.position.x + xOffset;
        float yPos = Camera.main.transform.position.y;

        if (LookAt.transform.position.y - Camera.main.transform.position.y > 4)
        {
            yPos = Camera.main.transform.position.y + .1f; // + .2f; //(LookAt.transform.position.y - Camera.main.transform.position.y - 4);
        }
        else if (LookAt.transform.position.y - Camera.main.transform.position.y < -4)
        {
            yPos = Camera.main.transform.position.y - (LookAt.transform.position.y + Camera.main.transform.position.y + 4);
        }
            
        Camera.main.transform.position = new Vector3(xPos, yPos, -10);
    }
}
