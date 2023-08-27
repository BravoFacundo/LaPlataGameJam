using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool pushed;

    [Header("References")]
    [SerializeField] private PusherBehaviour pusherBehaviour;

    private void Awake()
    {
        pusherBehaviour = transform.parent.parent.GetComponent<PusherBehaviour>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            pusherBehaviour.playerRB = collision.GetComponent<Rigidbody>();
            pusherBehaviour.PlayerEnter();
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (pusherBehaviour.isPushing && pusherBehaviour.playerRB != null && !pushed)
        {
            pusherBehaviour.PlayerPush();
            pushed = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            pusherBehaviour.playerRB = null;
            pushed = false;
        }
    }
}
