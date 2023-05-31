using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private CannonBehaviour cannonBehaviour;
    private bool pushed;

    private void Awake()
    {
        cannonBehaviour = transform.parent.parent.GetComponent<CannonBehaviour>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Rigidbody collisionRB = collision.gameObject.GetComponent<Rigidbody>();

        if (!cannonBehaviour.isMoving && collisionRB != null)
        {
            print("Trig");

            Vector3 forceDirection = transform.up;
            forceDirection.Normalize();

            collisionRB.AddForce(forceDirection * cannonBehaviour.pushForce, ForceMode.Impulse);
        }

    }
}
