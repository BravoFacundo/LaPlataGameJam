using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObjectRespawn : MonoBehaviour
{
    [Header("Configuration")]
    public float respawnYValue;
    [SerializeField] private float respawnDelay;
    private bool respawning;

    [Header("Debug")]
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private Transform parent;
    private Rigidbody rb;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        parent = transform.parent;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!respawning && transform.position.y < respawnYValue)
        {
            if (!respawning)
            {
                StartCoroutine(RespawnPuzzleObject());
            }
        }
    }
    private IEnumerator RespawnPuzzleObject()
    {
        respawning = true;
        yield return new WaitForSeconds(respawnDelay);

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
        transform.parent = parent;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        respawning = false;
    }

}
