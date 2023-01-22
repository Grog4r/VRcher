using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideText : MonoBehaviour
{

    private GameObject xrOrigin = null;
    private string text = "";
    private TextMesh textMesh = null;
    private float maxViewDistance = 80.0f;

    void Start()
    {
        xrOrigin = GameObject.Find("XR Origin");
        textMesh = GetComponent<TextMesh>();
        text = textMesh.text;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(xrOrigin.transform.position, this.transform.position);
        if (distanceToPlayer > maxViewDistance)
        {
            textMesh.text = "";
        }
        else
        {
            textMesh.text = text;
        }
    }
}
