using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketGoalDetection : MonoBehaviour
{
    [Header("Goal")]
    [SerializeField] string thisGoal;
    [SerializeField] bool justContactMode;
    [SerializeField] GameManager gameManager;

    [Header("Behaviour")]
    [SerializeField] private float cleanDelay = 5f;
    [SerializeField] Vector2 desiredOrder;
    [SerializeField] private List<int> triggerAlarms;

    private void Start()
    {
        
    }

    public void TriggerAlarm(int triggerOrder)
    {
        triggerAlarms.Add(triggerOrder);
        StartCoroutine(CheckAlarms());
    }

    private IEnumerator CheckAlarms()
    {
        if (!justContactMode)
        {
            for (int i = 0; i < triggerAlarms.Count; i++)
            {
                int currentNumber = triggerAlarms[i];
                if (currentNumber == desiredOrder.y)
                {
                    if (i > 0 && triggerAlarms[i - 1] == desiredOrder.x)
                    {
                        gameManager.AddGoalToList(thisGoal);
                        Destroy(transform.GetChild(1));
                    }
                }
            }
        }
        for (int i = 0; i < triggerAlarms.Count; i++)
        {            
            int currentNumber = triggerAlarms[i];
            if (currentNumber == desiredOrder.x || currentNumber == desiredOrder.y)
            {
                gameManager.AddGoalToList(thisGoal);
                Destroy(transform.GetChild(1).gameObject);
            }
        }

        yield return new WaitForSeconds(cleanDelay);
        triggerAlarms.Clear();
    }
}
