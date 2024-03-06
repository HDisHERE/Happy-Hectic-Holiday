using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int num = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Coin")//Or use comparetag function
        {
            num++;
            Destroy(collision.gameObject);
            Debug.Log("Coin!"+num);
        }
    }
}
