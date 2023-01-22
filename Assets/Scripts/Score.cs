using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class Score : MonoBehaviour
{
    private TextMesh textMesh = null;
    private int totalScore = 0;

    public int targetScore = 10;

    public bool finished = false;

    public GameObject targetObject = null;
    private Vector3 resetPosition = Vector3.zero;

    private GameObject xrOrigin = null;

    public static float visibilityDistance = 80.0f;

    private string text = "";

    void Start()
    {
        resetPosition = targetObject.transform.position;
        xrOrigin = GameObject.Find("XR Origin");
    }

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        UpdateScore(0);
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(this.transform.position, xrOrigin.transform.position);
        
        if (distanceToPlayer > visibilityDistance) {
            textMesh.text = "";
        }
        else {
            textMesh.text = text;
        }
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        text = "(" + totalScore + " / " + targetScore + ")";
        textMesh.text = text;
        if (totalScore >= targetScore) {
            finished = true;
            textMesh.color = Color.green;
        } else {
            finished = false;
            textMesh.color = Color.red;
        }
    }

    public void ResetScore() {
        print("Resetting Target Score.");
        UpdateScore(-this.totalScore);
        finished = false;
        targetObject.transform.position = resetPosition;
    }
}
