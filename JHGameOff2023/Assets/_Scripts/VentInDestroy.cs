using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInDestroy : MonoBehaviour
{
    public bool canDestroy = false;
    
    private void OnEnable()
    {
        PlayerQTEController.OnQTEWin += Destroy;
    }

    private void OnDisable()
    {
        PlayerQTEController.OnQTEWin -= Destroy;
    }

    private void Destroy()
    {
        if (canDestroy) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDestroy = true;
        }
    }
}
