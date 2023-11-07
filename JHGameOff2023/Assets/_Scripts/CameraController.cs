using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform LookAt;
    public float XOffset;


    private void FixedUpdate()
    {
        transform.position = new Vector3(LookAt.position.x + XOffset, transform.position.y, transform.position.z);
    }
}
