using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScore : MonoBehaviour
{
    private TextMesh textMesh = null;
    private int totalScore = 0;

    public int targtetScore = 10;
    
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        textMesh.text = "Score: " + score.ToString() + "\nTotal Score: " + totalScore.ToString();
        if (totalScore >= targtetScore) {
            textMesh.color = Color.green;
        }
    }
}
