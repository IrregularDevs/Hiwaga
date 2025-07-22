using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    public Transform cameraTransform;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;
    public bool canMove = true;
    public bool enableJump = false; // Toggle jump in Inspector

    public Vector3 CurrentMoveDirection { get; private set; }

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!canMove)
        {
            controller.Move(Vector3.zero);
            return;
        }

        // Ground check
        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Movement speed (sprint toggle)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;

            Vector3 moveDirection = (camForward * inputDirection.z + camRight * inputDirection.x).normalized;

            CurrentMoveDirection = moveDirection;

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            // Rotation (unaffected by sprinting)
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            CurrentMoveDirection = Vector3.zero;
        }

        // Jump (toggleable)
        if (enableJump && Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Gravity application
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // In-game menu toggle (ESC)
        if (Input.GetButtonDown("Esc") && SceneManager.GetActiveScene().name != "MainMenuUI")
        {
            OptionsManager.Instance.OpenCloseMenu(!OptionsManager.Instance.GetMenuStateSelf());

            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked)
                ? CursorLockMode.None
                : CursorLockMode.Locked;
        }
    }
}
