using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f; // Increases gravity when falling
    public float lowJumpMultiplier = 2f; // Increases gravity when jump button is released early
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool canJump = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check using CharacterController's built-in isGrounded
        bool isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            velocity.y = -0.1f; // Prevents hovering effect
            canJump = true; // Reset jump ability when grounded
        }

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (isGrounded && canJump && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            canJump = false; // Disable jumping until grounded again
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