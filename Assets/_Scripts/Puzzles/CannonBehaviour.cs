using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    [Header("Debug")]
    public bool isMoving = true;

    [Header("Configuration")]
    public float pushForce = 20f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float retractionSpeed = 2.0f;
    private float startTime;

    [Header("References")]
    [SerializeField] private Transform MovingPart;
    [SerializeField] private Transform pivot;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private Quaternion finalRotation;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (isMoving)
        {
            float currentTime = Time.time - startTime;
            float t = currentTime / rotationSpeed;

            MovingPart.localRotation = Quaternion.Lerp(startRotation, finalRotation, t);

            if (t >= 1.0f)
            {
                //startTime = Time.time;
                //isMoving = false;
            }
        }
        else
        {
            float currentTime = Time.time - startTime;
            float t = currentTime / retractionSpeed;

            MovingPart.localRotation = Quaternion.Lerp(finalRotation, startRotation, t);

            if (t >= 1.0f)
            {
                //startTime = Time.time;
                //isMoving = true;
            }
        }
    }
}
