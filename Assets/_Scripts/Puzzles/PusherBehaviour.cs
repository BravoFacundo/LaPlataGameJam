using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherBehaviour : MonoBehaviour
{
    [Header("Debug")]
    public bool isPushing = true;
    public Rigidbody playerRB;

    [Header("Configuration")]
    public float pushForce = 20f;
    public float cancelMovement = 2f;
    [SerializeField] private float pushingSpeed = 1.0f;
    [SerializeField] private float retractionSpeed = 2.0f;
    [SerializeField] private float startDelay = 0;
    private float startTime;

    [Header("References")]
    [SerializeField] private Transform MovingPart;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 finalPosition;

    private void Start()
    {
        isPushing = false;
        StartCoroutine(StartDelay(startDelay));
    }

    private IEnumerator StartDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        startTime = Time.time;
        isPushing = true;
    }

    public void PlayerPush()
    {
        Vector3 forceDirection = transform.right;
        forceDirection.Normalize();

        playerRB.velocity = Vector3.zero;
        playerRB.AddForce(forceDirection * pushForce, ForceMode.VelocityChange);

        StartCoroutine(playerRB.GetComponent<PlayerController>().PausePlayerMovement(cancelMovement));
    }

    private void Update()
    {
        if (isPushing)
        {
            float currentTime = Time.time - startTime;
            float t = currentTime / pushingSpeed;

            MovingPart.localPosition = Vector3.Lerp(startPosition, finalPosition, t);

            if (t >= 1.0f)
            {
                startTime = Time.time;
                isPushing = false;
            }
        }
        else
        {
            float currentTime = Time.time - startTime;
            float t = currentTime / retractionSpeed;

            MovingPart.localPosition = Vector3.Lerp(finalPosition, startPosition, t);

            if (t >= 1.0f)
            {
                startTime = Time.time;
                isPushing = true;
            }
        }
    }
}