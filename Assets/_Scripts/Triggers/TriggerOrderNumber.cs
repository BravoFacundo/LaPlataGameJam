using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOrderNumber : MonoBehaviour
{
    [SerializeField] private int triggerOrderNumber;
    private CrossGoalDetection basketManager;

    private void Start()
    {
        basketManager = transform.parent.GetComponent<CrossGoalDetection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            basketManager.TriggerAlarm(triggerOrderNumber);
        }
    }
}
