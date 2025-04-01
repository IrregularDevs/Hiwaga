using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public LayerMask groundMask;
    public float rotationSpeed = 10f;
    public Transform model; // Assign the model to rotate independently

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool jumpRequest = false;
    private float groundCheckDistance = 0.2f;

    public bool canMove = true;

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

        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Rotate only the model to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (jumpRequest)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpRequest = false;
        }

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
