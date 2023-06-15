using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SizeData
{
    public string size;
    public float ballScale;
    public float ballWeight;
    public float moveSpeed;
    public float jumpForce;

    public float cameraDistance;
    public float cameraFOV;
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] bool canMove = true;
    [SerializeField] bool canChangeSize = true;
    [SerializeField] float moveSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool canJump = true;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode growKey = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkKey = KeyCode.Mouse1;

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
        if (canMove)
        {
            InputHandling();
            SpeedControl();
        }

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    public IEnumerator PausePlayerMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    private void InputHandling()
    {
        if (canMove)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");          
        }

        if (Input.GetKey(jumpKey) && canJump) //&& readyToJump && grounded
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (canChangeSize)
        {

        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump() => canJump = true;

    private void MovePlayer()
    {
        var cameraRotation = cam.transform.rotation;
        Quaternion neutralRotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        moveDirection = neutralRotation * Vector3.forward * verticalInput + neutralRotation * Vector3.right * horizontalInput;
        rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
