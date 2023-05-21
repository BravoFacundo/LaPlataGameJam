using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    [Header("References")]
    [SerializeField] private Transform orientation; 
    [SerializeField] private Transform player; 
    [SerializeField] private Rigidbody rb;
    //[SerializeField] private Transform playerObj; 

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        /* This continuously adjusts the orientation of the PlayerObj to face forward, 
         * We don't need it because it disrupts the ball's spin.

        Vector3 inputDir = orientation.forward * VerticalInput + orientation.right * horizontalInput;
        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        */
    }
}
