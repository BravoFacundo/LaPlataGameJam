using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAlarm : MonoBehaviour
{
    [SerializeField] private int triggerOrderNumber;
    private GoalManager basketManager;

    private void Start()
    {
        basketManager = transform.parent.GetComponent<GoalManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            basketManager.TriggerAlarm(triggerOrderNumber);
        }
    }
}
