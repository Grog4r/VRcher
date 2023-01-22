using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    public GameObject movingObjects = null;
    public GameObject[] waypoints = null;
    public float speed = 1.0f;

    private Vector3 currentPosition = Vector3.zero;
    private int waypointIndex = 0;

    private Vector3 resetPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length > 1)
        {
            currentPosition = movingObjects.transform.position;
            movingObjects.transform.position = Vector3.MoveTowards(currentPosition, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

            float distance = Vector3.Distance(currentPosition, waypoints[waypointIndex].transform.position);
            if (distance <= 0.3)
            {
                if (waypointIndex + 1 < waypoints.Length)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
            }
        }
    }

    public void ResetTargetPosition() {
        this.transform.position = resetPosition;
    }
}
