using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private CannonBehaviour cannonBehaviour;
    private float triggerEnterDelay;

    private void Awake()
    {
        cannonBehaviour = transform.parent.parent.GetComponent<CannonBehaviour>();
        triggerEnterDelay = cannonBehaviour.actionDelay;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TriggerEnterDelay(triggerEnterDelay));
            cannonBehaviour.playerRB = collision.GetComponent<Rigidbody>();
        }
    }
    private IEnumerator TriggerEnterDelay(float triggerEnterDelay)
    {
        yield return new WaitForSeconds(triggerEnterDelay);
        cannonBehaviour.PlayerEnterCannon();

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(nameof(TriggerEnterDelay));
            StartCoroutine(cannonBehaviour.PlayerExitCannon());
            cannonBehaviour.playerRB = null;
        }
    }
}
