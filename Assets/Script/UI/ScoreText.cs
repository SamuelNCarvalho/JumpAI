using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.gameOver)
        {
            scoreText.text = "SCORE: " + Mathf.RoundToInt(GameManager.instance.score);
        }
    }
}
