using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void CoinBehaviour();
    public static CoinBehaviour OnCoinCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnCoinCollision();
            Destroy(this.gameObject);
        }
    }
}
