using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    // Score text
    public TextMeshProUGUI scoreText;
    // Score starts at 0
    private int score = 0;

    public int coinNum = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Coin")//Or use comparetag function
        {
            coinNum++;

            // Increase score
            score++;
            scoreText.text = "Score: " + score.ToString();

            Destroy(collision.gameObject);
            Debug.Log("Coin!"+ coinNum);
        }
    }
}
