using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    Text scoreText;
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "Total:" + score.ToString();
    }

    public void ScoreHit(int scorePerHit)
    {
        score = score + scorePerHit;
        scoreText.text = "Total:" + score.ToString();
    }
}
