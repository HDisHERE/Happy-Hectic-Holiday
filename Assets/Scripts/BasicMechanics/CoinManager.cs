using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinNum = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Coin")//Or use comparetag function
        {
            coinNum++;
            Destroy(collision.gameObject);
            Debug.Log("Coin!"+ coinNum);
        }
    }
}
