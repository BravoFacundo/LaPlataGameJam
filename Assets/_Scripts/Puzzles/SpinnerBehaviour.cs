using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehaviour : MonoBehaviour
{
    [Header("Configuration")]
    public float rotationSpeed = 1f;
    public bool reverseRotation = true;

    [Header("References")]
    [SerializeField] private Transform pivotObject;    
    private Rigidbody childRigidbody;

    void Start()
    {
        childRigidbody = pivotObject.GetComponent<Rigidbody>();

        // Add Rigidbody component if not already attached
        if (childRigidbody == null)
        {
            childRigidbody = pivotObject.gameObject.AddComponent<Rigidbody>();
        }

        // Set Rigidbody properties
        childRigidbody.isKinematic = true;
        childRigidbody.useGravity = false;

    }

    void FixedUpdate()
    {
        if (childRigidbody != null)
        {
            float direction = reverseRotation ? -1f : 1f;

            Quaternion rotationDelta = Quaternion.Euler(direction * rotationSpeed * Time.fixedDeltaTime * Vector3.right);
            childRigidbody.MoveRotation(childRigidbody.rotation * rotationDelta);
        }
    }
}
