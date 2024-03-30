using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickingScript : MonoBehaviour
{
    // CLICKER
    public TMP_Text scoreText;
    public float currentScore;
    public float scoreUp;


    void Start()
    {
        currentScore = 0;
        scoreUp = 1;
    }

    void Update()
    {
        scoreText.text = currentScore.ToString();
    }

    public void click()
    {
        currentScore += scoreUp;
    }
}
