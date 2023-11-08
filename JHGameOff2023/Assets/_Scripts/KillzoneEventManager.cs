using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneEventManager : MonoBehaviour
{
    public delegate void Death();
    public static event Death OnDeathEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (OnDeathEnter != null)
            {
                OnDeathEnter();
            }
        }
    }
}
