using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstruction : MonoBehaviour
{
    public GameObject instruction;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            instruction.SetActive(true);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            instruction.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
