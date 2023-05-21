using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBehaviour : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public bool reverseRotation = true;

    private Transform firstChild;
    private Rigidbody childRigidbody;

    void Start()
    {
        if (transform.childCount > 0)
        {
            firstChild = transform.GetChild(0);
            childRigidbody = firstChild.GetComponent<Rigidbody>();

            // Add Rigidbody component if not already attached
            if (childRigidbody == null)
            {
                childRigidbody = firstChild.gameObject.AddComponent<Rigidbody>();
            }

            // Set Rigidbody properties
            childRigidbody.isKinematic = true;
            childRigidbody.useGravity = false;
        }
            
    }

    void FixedUpdate()
    {
        if (childRigidbody != null)
        {
            float direction = reverseRotation ? -1f : 1f;

            // Apply rotation to the child Rigidbody on the X-axis
            Quaternion rotationDelta = Quaternion.Euler(Vector3.right * rotationSpeed * direction * Time.fixedDeltaTime);
            childRigidbody.MoveRotation(childRigidbody.rotation * rotationDelta);
        }
    }
}
