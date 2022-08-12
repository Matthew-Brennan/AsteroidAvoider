using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    private float score;
    private bool incrementScore = true;

    // Update is called once per frame
    void Update()
    {
        if(!incrementScore) {return;}
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int EndScore()
    {
        incrementScore = false;
        scoreText.text = string.Empty;
        return  Mathf.FloorToInt(score);

    }

    public void StartTimer()
    {
            incrementScore = true;
    }
}
