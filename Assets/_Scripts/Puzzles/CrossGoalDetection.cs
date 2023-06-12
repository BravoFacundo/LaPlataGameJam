using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossGoalDetection : MonoBehaviour
{
    [Header("Goal")]
    [SerializeField] string thisGoal;
    [SerializeField] GameManager gameManager;

    public void CrossTrigger()
    {
        gameManager.AddGoalToList(thisGoal);
        Destroy(transform.Find("Trigger").gameObject);
    }
}
