using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    ArrayList scoresArray = new ArrayList();

    public GameObject portal = null;

    // Start is called before the first frame update
    void Awake()
    {   
        CollectTargets();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoresArray.Count == 0)
        {
            LevelDone();
        }
        else
        {
            LevelNotDone();
            ArrayList toRemove = new ArrayList();
            foreach (Score score in scoresArray)
            {
                if (score.finished)
                {
                    toRemove.Add(score);
                }
            }
            foreach (Score score in toRemove)
            {
                scoresArray.Remove(score);
            }
        }
    }

    public void CollectTargets()
    {
        int targetCount = this.transform.childCount;
        for (int i = 0; i < targetCount; i++) {
            Transform target = this.transform.GetChild(i);
            // print(target.name);
            Score score = target.GetChild(0).GetComponentInChildren<Score>();
            // print(score.targetScore);
            scoresArray.Add(score);
        }
    }

    void LevelDone()
    {
        portal.transform.GetChild(0).gameObject.SetActive(true);
    }

    void LevelNotDone()
    {
        portal.transform.GetChild(0).gameObject.SetActive(false);
    }
}
