using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    [Header("References")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        orientation = transform.GetChild(1);
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputHandling();
        SpeedControl();

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void InputHandling()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        var cameraRotation = cam.transform.rotation;
        Quaternion neutralRotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        moveDirection = neutralRotation * Vector3.forward * verticalInput + neutralRotation * Vector3.right * horizontalInput;
        rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }
}
