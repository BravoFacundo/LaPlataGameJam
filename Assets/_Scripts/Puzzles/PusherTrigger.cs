using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private PusherBehaviour pusherBehaviour;

    [Header("Configuration")]
    [SerializeField] private bool triggerMode;
    private MeshCollider meshCollider;
    private bool pushed;

    private void Awake()
    {
        pusherBehaviour = transform.parent.parent.GetComponent<PusherBehaviour>();

        meshCollider = GetComponent<MeshCollider>();
        if (triggerMode) meshCollider.isTrigger = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody collisionRB = collision.gameObject.GetComponent<Rigidbody>();

        if (pusherBehaviour.isPushing && collisionRB != null)
        {
            print("coli");

            Vector3 forceDirection = collision.contacts[0].point - transform.position;
            forceDirection.Normalize();

            collisionRB.AddForce(forceDirection * pusherBehaviour.pushForce, ForceMode.Impulse);
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        Rigidbody collisionRB = collision.gameObject.GetComponent<Rigidbody>();

        if (pusherBehaviour.isPushing && collisionRB != null) //&& !pushed
        {
            print("Trig");

            Vector3 forceDirection = pusherBehaviour.transform.right;
            forceDirection.Normalize();

            collisionRB.AddForce(forceDirection * pusherBehaviour.pushForce, ForceMode.Impulse);

            //meshCollider.enabled = false;
            //pushed = true;
        }

    }
    /*
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            meshCollider.enabled = true;
            pushed = false;
        }

    }
    */
}
