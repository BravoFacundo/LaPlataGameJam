using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingGoalHandler : MonoBehaviour
{
    [Header("Goal")]
    [SerializeField] string thisGoal;
    [SerializeField] GameManager gameManager;

    [Header("Behaviour")]
    [SerializeField] private List<GameObject> pinObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            pinObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            pinObjects.Remove(other.gameObject);

            if (pinObjects.Count == 0)
            {
                AllPinesExited();
            }
        }
    }

    private void AllPinesExited()
    {
        gameManager.AddGoalToList(thisGoal);
    }
}
