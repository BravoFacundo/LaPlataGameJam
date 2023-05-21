using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> GoalList;
    [SerializeField] private List<string> currentGoalList;

    private void Start()
    {
        CheckGoalList();
    }

    public void AddGoalToList(string goalToAdd)
    {
        currentGoalList.Add(goalToAdd);
        CheckGoalList();
    }

    private void CheckGoalList()
    {
        bool contieneTodos = true;
        foreach (string str in GoalList)
        {
            if (!currentGoalList.Contains(str))
            {
                contieneTodos = false;
                break;
            }
        }

        if (contieneTodos)
        {
            Debug.Log("La lista contiene todos los strings deseados");
        }
        else
        {
            Debug.Log("La lista no contiene todos los strings deseados");
        }
    }
}
