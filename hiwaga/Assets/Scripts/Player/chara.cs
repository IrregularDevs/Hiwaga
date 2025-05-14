using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;
    public DebugDisplay debugDisplay;

    [Header("Jump Cooldown")]
    public float jumpCooldown = 0.5f;
    private float jumpCooldownTimer = 0f;

    [Header("Control Flags")]
    public bool canMove = true;

    public Vector3 CurrentMoveDirection { get; private set; }

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool jumpRequest = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!canMove)
        {
            controller.Move(Vector3.zero);
            return;
        }

        // Handle jump cooldown
        if (jumpCooldownTimer > 0f)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        // Check if grounded
        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -0.1f; // Stick to ground
            debugDisplay.SetText("Player is grounded");
        }

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded && jumpCooldownTimer <= 0f)
        {
            jumpRequest = true;
            jumpCooldownTimer = jumpCooldown; // Reset cooldown
            debugDisplay.SetText("Space key was pressed");
        }

        // Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        CurrentMoveDirection = moveDirection;

        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Jumping logic
        if (jumpRequest)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // Apply jump force
            jumpRequest = false; // Reset jump request after applying
        }

        // Apply gravity
        if (velocity.y < 0f)
        {
            velocity.y += gravity * (fallMultiplier - 1f) * Time.deltaTime; // Increased fall speed
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0f)
        {
            velocity.y += gravity * (lowJumpMultiplier - 1f) * Time.deltaTime; // Adjust jump speed when releasing button
        }

        // Always apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement based on velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
