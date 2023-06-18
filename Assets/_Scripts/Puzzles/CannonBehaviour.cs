using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    [Header("Debug")]
    public bool isMoving = true;
    public Rigidbody playerRB;

    [Header("Configuration")]
    public float pushForce = 20f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float retractionSpeed = 2.0f;
    public float actionDelay = 1.5f;
    public float cancelMovement = 2f;
    private float startTime;
    private bool actionLock;

    [Header("References")]
    [SerializeField] private Transform MovingPart;
    [SerializeField] private Transform pivot;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private Quaternion finalRotation;

    private void Start()
    {
        startTime = Time.time;
    }

    public void PlayerEnterCannon()
    {
        startTime = Time.time;
        isMoving = true;
    }
    public IEnumerator PlayerExitCannon()
    {
        yield return new WaitForSeconds(actionDelay);
        startTime = Time.time;
        isMoving = false;
        actionLock = false;
    }
    public IEnumerator PlayerLaunch()
    {
        yield return new WaitForSeconds(actionDelay);

        Vector3 forceDirection = MovingPart.forward;
        forceDirection.Normalize();
        playerRB.transform.position = pivot.position;
        playerRB.AddForce(forceDirection * pushForce, ForceMode.VelocityChange);

        StartCoroutine(playerRB.GetComponent<PlayerController>().PausePlayerMovement(cancelMovement));
        StartCoroutine(nameof(PlayerExitCannon));
    }

    private void Update()
    {
        if (isMoving)
        {
            float currentTime = Time.time - startTime;
            float t = currentTime / rotationSpeed;

            MovingPart.localRotation = Quaternion.Lerp(startRotation, finalRotation, t);

            if (t >= 1.0f && !actionLock)
            {
                StartCoroutine(PlayerLaunch());            
                actionLock = true;
            }
        }
        else
        {
            float currentTime = Time.time - startTime;
            float t = currentTime / retractionSpeed;

            MovingPart.localRotation = Quaternion.Lerp(finalRotation, startRotation, t);
        }
    }
}
