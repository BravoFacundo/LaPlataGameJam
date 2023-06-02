using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGoalDetection : MonoBehaviour
{
    [Header("Goal")]
    [SerializeField] string thisGoal;
    [SerializeField] GameManager gameManager;

    [Header("Behaviour")]
    [SerializeField] private List<GameObject> insideObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RemovableGoal"))
        {
            insideObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RemovableGoal"))
        {
            insideObjects.Remove(other.gameObject);

            if (insideObjects.Count == 0)
            {
                AllPinesExited();
            }
        }
    }

    private void AllPinesExited()
    {
        gameManager.AddGoalToList(thisGoal);
        Destroy(gameObject);
    }
}
