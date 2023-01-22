using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreBoard : MonoBehaviour
{
    private TextMesh textMesh = null;
    private GameObject xrOrigin = null;
    private static bool timerStarted = false;
    public static bool timerFinished = false;

    private static float startTime = 0.0f;
    private static float endTime = 0.0f;
    private static int totalArrows = 0;

    private string text = "";
    private float visibilityDistance = 80.0f;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        xrOrigin = GameObject.Find("XR Origin");
    }

    void Update()
    {
        if (timerStarted)
        {
            if (!timerFinished)
            {
                endTime = Time.realtimeSinceStartup;
            }

            float passedTime = endTime - startTime;
            int passedMinutes = (int)(passedTime / 60);
            float passedSeconds = (passedTime % 60);

            text = string.Format("Time: {0:00}:{1:00.00}\n", passedMinutes, passedSeconds);
            text += string.Format("Arrows: {0}", totalArrows);
            textMesh.text = text;
        } else {
            text = "Time: 00:00.00\nArrows: 0";
            textMesh.text = text;
        }

        float distanceToPlayer = Vector3.Distance(this.transform.position, xrOrigin.transform.position);
        if (distanceToPlayer > visibilityDistance) {
            textMesh.text = "";
        }
        else {
            textMesh.text = text;
        }
    }

    public static void AddArrow()
    {
        print("Arrow Added.");
        if (!timerStarted)
        {
            print("Timer started.");
            startTime = Time.realtimeSinceStartup;
            timerStarted = true;
            totalArrows++;
        }
        else
        {
            totalArrows++;
        }
    }

    public static void ResetScoreBoard()
    {
        timerStarted = false;
        timerFinished = false;
        startTime = 0.0f;
        endTime = 0.0f;
        totalArrows = 0;
        print("Score Reset.");
    }
}
