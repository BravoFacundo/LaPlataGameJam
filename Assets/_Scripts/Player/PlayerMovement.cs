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
    
    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump = true;

    [Header("Drag")]
    [SerializeField] private float groundDrag;

    [Header("Size")]
    public int size = 2;
    [SerializeField] private bool canChange = true;
    [SerializeField] float changeCooldown;
    [SerializeField] private bool canGrow = true;
    [SerializeField] private bool canShrink = true;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode growKey = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkKey = KeyCode.Mouse1;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float playerHeight;
    [SerializeField] private bool grounded;

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle;
    private RaycastHit slopeHit;

    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cam;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform.GetChild(1);
        cam = Camera.main.transform;
    }

    private void Update()
    {
        playerHeight = transform.localScale.y;
        grounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHeight*0.55f ,0) , playerHeight * .05f, groundLayer);

        InputHandling();
        SpeedControl();

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void InputHandling()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && grounded) //&& readyToJump && grounded
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(growKey) && size < 3 && canChange)
        {
            transform.localScale *= 5f;
            size++;
            canChange = false;
            Invoke(nameof(ResetChange), changeCooldown);
        }
        if (Input.GetKey(shrinkKey) && size > 1 && canChange)
        {
            transform.localScale *= .2f;
            size--;
            canChange = false;
            Invoke(nameof(ResetChange), changeCooldown);
        }
    }

    private void MovePlayer()
    {
        var cameraRotation = cam.transform.rotation;
        Quaternion neutralRotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        moveDirection = neutralRotation * Vector3.forward * verticalInput + neutralRotation * Vector3.right * horizontalInput;

        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
        }

        if (grounded)
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        else 
        if (!grounded)
            rb.AddForce(10f * airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Force);
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

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
    }

    //------------------------------Invokes-----------------------------------------//

    private void ResetJump()
    {
        readyToJump = true;
    }
    private void ResetChange()
    {
        canChange = true;
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * .5f + .3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
