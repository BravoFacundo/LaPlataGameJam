using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> GoalList;
    [SerializeField] private List<string> currentGoalList;

    [Header("References")]
    [SerializeField] private MusicManager musicManager;

    private void Start()
    {
        CheckGoalList();
    }

    public void AddGoalToList(string goalToAdd)
    {
        if (!currentGoalList.Contains(goalToAdd)) currentGoalList.Add(goalToAdd);
        CheckGoalList();
    }

    private void CheckGoalList()
    {
        bool hasAllGoals = true;
        foreach (string str in GoalList)
        {
            if (!currentGoalList.Contains(str))
            {
                hasAllGoals = false;
                break;
            }
            if (currentGoalList.Contains("Tutorial"))
            {
                musicManager.PlayMusicClip("Original");
            }
        }

        if (hasAllGoals)
        {
            musicManager.PlayMusicClip("Final");
        }
        else
        {
            //Debug.Log("La lista no contiene todos los strings deseados");
        }
    }
 
}
