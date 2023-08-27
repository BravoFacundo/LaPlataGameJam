using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoalData
{
    public string goalType;
    public int maxGoalCount;
}

public class GameManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool startWithTutorial;
    [SerializeField] private bool startWithAllCompleted;
    public bool gameCompleted;

    [Header("Goals")]
    [SerializeField] private List<GoalData> currentGoals = new List<GoalData>();
    [SerializeField] private List<GoalData> objectiveGoals = new List<GoalData>();
    private bool tutorialCompleted = false;

    [Header("References")]
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private ChangeAreaColor areaColorManager;

    private void Start()
    {
        InitializeCurrentGoals();

        if (startWithAllCompleted)
        {
            currentGoals = objectiveGoals;
            CheckGoalList();
        }
        else
        {
            if (startWithTutorial) AddGoalToList("Tutorial");
            else CheckGoalList();
        }
    }

    private void InitializeCurrentGoals()
    {
        foreach (var goal in objectiveGoals)
        {
            GoalData newGoal = new GoalData();
            newGoal.goalType = goal.goalType;
            newGoal.maxGoalCount = 0;
            currentGoals.Add(newGoal);
        }
    }

    public void AddGoalToList(string goalType)
    {
        GoalData objectiveGoal = objectiveGoals.Find(x => x.goalType == goalType);
        if (objectiveGoal != null)
        {
            GoalData currentGoal = currentGoals.Find(x => x.goalType == goalType);
            int maxGoalCount = objectiveGoal.maxGoalCount;
            int currentGoalCount = currentGoal.maxGoalCount;
            if (currentGoalCount < maxGoalCount)
            {
                currentGoal.maxGoalCount++;
                CheckGoalList();

                if (currentGoalCount + 1 == maxGoalCount)
                {
                    NewAreaCompleted(goalType);
                }
                else
                {
                    NewObjectiveCompleted(goalType);
                }
            }
            
        }
    }

    private void CheckGoalList()
    {
        bool hasAllGoals = true;
        foreach (var goal in objectiveGoals)
        {
            GoalData currentGoal = currentGoals.Find(x => x.goalType == goal.goalType);
            if (currentGoal.maxGoalCount < goal.maxGoalCount)
            {
                hasAllGoals = false;
                break;
            }
        }

        if (hasAllGoals)
        {
            AllAreasCompleted();
        }

        GoalData tutorialGoal = currentGoals.Find(x => x.goalType == "Tutorial");
        if (tutorialGoal != null && tutorialGoal.maxGoalCount == 1 && !tutorialCompleted)
        {
            TutorialCompleted();
        }
    }

    private void TutorialCompleted()
    {
        print("Tutorial Completed");
        tutorialCompleted = true;
        areaColorManager.ChangeMaterialColor("Tutorial");
        musicManager.PlayMusicClip("Original");
    }

    private void NewObjectiveCompleted(string goalType)
    {
        print("New Objetive Completed: " + goalType);
    }

    private void NewAreaCompleted(string goalType)
    {
        print("New Area Completed: " + goalType);
        areaColorManager.ChangeMaterialColor(goalType);
    }

    private void AllAreasCompleted()
    {
        print("All Area Completed");
        gameCompleted = true;
        musicManager.PlayMusicClip("Final");
    }

}
