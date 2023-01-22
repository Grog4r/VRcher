using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLevels : MonoBehaviour
{

    public Weapon[] weapons = null;
    public GameObject[] targetGroups = null;
    public GameObject arrows = null;


    private ArrayList scoresArray = new ArrayList();
    private ArrayList targetsArray = new ArrayList();
    private ArrayList levelHandlers = new ArrayList();

    void Awake()
    {

        foreach (GameObject targetGroup in targetGroups)
        {
            int targetGroupSize = targetGroup.transform.childCount;

            for (int i = 0; i < targetGroupSize; i++)
            {   
                Transform target = targetGroup.transform.GetChild(i);
                print(target.name);

                TargetHandler targetHandler = target.GetComponent<TargetHandler>();
                targetsArray.Add(targetHandler);

                Score score = target.GetChild(0).GetComponentInChildren<Score>();
                // print(score.targetScore);
                scoresArray.Add(score);
            }

            levelHandlers.Add(targetGroup.GetComponent<LevelHandler>());
        }
    }


    public void ResetLevels()
    {
        // Reset Scoreboards
        ScoreBoard.ResetScoreBoard();

        // Reset Crossbow Positions
        foreach (Weapon weapon in weapons)
        {
            weapon.ResetWeapon();
        }

        // Reset Targets
        foreach (Score score in scoresArray)
        {
            score.ResetScore();
        }

        // Disable Portals again
        foreach (LevelHandler levelHandler in levelHandlers)
        {
            levelHandler.CollectTargets();
        }

        DeleteArrows();
    }

    public void StopScoreBoardTimers()
    {
        ScoreBoard.timerFinished = true;
    }

    private void DeleteArrows()
    {
        foreach (Transform arrow in arrows.transform)
        {
            GameObject.Destroy(arrow.gameObject);
        }
    }
}
