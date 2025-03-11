using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public LayerMask groundMask;
    public float rotationSpeed = 10f; // Speed of turning

    private CharacterController controller;
    private Vector3 velocity;
    private bool canJump = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        bool isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            velocity.y = -0.1f;
            canJump = true;
        }

        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal"); // Use GetAxisRaw for instant stop
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Stop movement if no input is detected
        if (moveDirection.magnitude > 0.1f)
        {
            // Rotate character to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Move character in the direction it is facing
            Vector3 forwardMove = transform.forward * moveSpeed * Time.deltaTime;
            controller.Move(forwardMove * moveDirection.magnitude); // Prevent diagonal speed boost
        }

        // Jumping
        if (isGrounded && canJump && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            canJump = false;
        }

        // Apply gravity with smooth jump and fall
        if (velocity.y < 0)
        {
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
