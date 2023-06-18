using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class BallSizeData
{
    public string size;

    public float scale;
    public float mass;

    public float moveSpeed;
    public float jumpForce;

    //public float cameraDistance;
    public float cameraFOV;
}

public class PlayerController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool canMove = true;
    [SerializeField] bool canChangeSize = true;
    [SerializeField] bool canJump = true;
    [SerializeField] bool limitSpeed = true;

    [Header("Configuration")]
    [SerializeField] private int currentSizeIndex = 1;
    [SerializeField] private float changeSizeCooldown;
    [SerializeField] private List<BallSizeData> ballSize;

    [Header("Movement")]
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    [SerializeField] float moveSpeed; //Al terminar deberia no hacer falta

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode growKey = KeyCode.Mouse0;
    [SerializeField] KeyCode shrinkKey = KeyCode.Mouse1;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cam;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform.Find("Orientation");

        cam = Camera.main.transform;
        cinemachineFreeLook = cam.transform.GetChild(0).GetComponent<CinemachineFreeLook>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        SetBallSizeDefaultValues();
        StartCoroutine(ChangeSize());
    }
    private void Update()
    {
        InputHandling();
        SpeedControl();
    }
    private void FixedUpdate() => MovePlayer();

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

        if (Input.GetKey(jumpKey) && canJump) //grounded
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (canChangeSize)
        {
            if (Input.GetKey(shrinkKey))
            {
                if (currentSizeIndex > 0)
                {
                    currentSizeIndex--;
                    StartCoroutine(ChangeSize());
                    canChangeSize = false;
                    Invoke(nameof(ResetChangeSize), changeSizeCooldown);
                }
            }
            else if (Input.GetKey(growKey))
            {
                if (currentSizeIndex < 2)
                {
                    currentSizeIndex++;
                    StartCoroutine(ChangeSize());
                    canChangeSize = false;
                    Invoke(nameof(ResetChangeSize), changeSizeCooldown);
                }
            }
        }
    }

    private IEnumerator ChangeSize()
    {
        cinemachineFreeLook.m_Lens.FieldOfView = ballSize[currentSizeIndex].cameraFOV;
        yield return new WaitForEndOfFrame();
        transform.localScale = Vector3.one * ballSize[currentSizeIndex].scale;
        rb.mass = ballSize[currentSizeIndex].mass;
    }
    private void ResetChangeSize() => canChangeSize = true;

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce( Vector3.up * ballSize[currentSizeIndex].jumpForce, ForceMode.Impulse);
    }
    private void ResetJump() => canJump = true;

    private void MovePlayer()
    {
        var cameraRotation = cam.transform.rotation;
        Quaternion neutralRotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        moveDirection = neutralRotation * Vector3.forward * verticalInput + neutralRotation * Vector3.right * horizontalInput;
        rb.AddForce(10f * ballSize[currentSizeIndex].moveSpeed * moveDirection.normalized, ForceMode.Force);
    }
    private void SpeedControl()
    {
        if (limitSpeed)
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    void SetBallSizeDefaultValues()
    {
        if (ballSize.Count != 3)
        {
            while (ballSize.Count < 3)
            {
                ballSize.Add(new BallSizeData
                {
                    size = "Medium",
                    scale = 1f,
                    mass = 10f,
                    moveSpeed = 12f,
                    jumpForce = 80f,
                });
            }
            ballSize[0].size = "Small";
            ballSize[2].size = "Big";
        }
    }

}
