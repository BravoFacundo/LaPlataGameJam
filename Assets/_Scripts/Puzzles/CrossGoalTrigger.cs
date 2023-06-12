using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossGoalTrigger : MonoBehaviour
{
    private CrossGoalDetection crossGoalDetection;

    private void Start()
    {
        crossGoalDetection = transform.parent.GetComponent<CrossGoalDetection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crossGoalDetection.CrossTrigger();
        }
    }
}
