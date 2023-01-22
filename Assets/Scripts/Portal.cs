using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportPosition = null;

    public XROrigin playerXROrigin = null;

    private bool playerIsInTeleporter = false;

    public bool reset = false;
    public bool stopTimer = false;

    ControlLevels levelControl = null;

    void Start()
    {
        levelControl = FindObjectOfType<ControlLevels>();
        print(levelControl);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInTeleporter)
        {
            if (reset) {
                levelControl.ResetLevels();
            }
            if (stopTimer) {
                levelControl.StopScoreBoardTimers();
            }
            playerXROrigin.transform.position = teleportPosition.position;
            playerXROrigin.transform.rotation = teleportPosition.rotation;
        }
    }

    void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
            playerIsInTeleporter = true;
		}
	}

    void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
            playerIsInTeleporter = false;
		}
	}
}
