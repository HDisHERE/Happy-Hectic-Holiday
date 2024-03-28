using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    // Score starts at 0
    private int score = 0;
    // Score text
    public TextMeshProUGUI scoreText;

    void OnTriggerEnter2D(Collider collision)
    {
        if(gameObject.tag == "Coin" )
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }
}
