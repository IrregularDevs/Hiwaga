using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;
    //public DebugDisplay debugDisplay;

    [Header("Control Flags")]
    public bool canMove = true;

    public Vector3 CurrentMoveDirection { get; private set; }

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

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

        // Check if grounded
        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -0.1f; // Stick to ground
            //debugDisplay.SetText("Player is grounded");
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

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement based on velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
