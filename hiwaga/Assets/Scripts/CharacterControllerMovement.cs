using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 2.0f; // Adjust as needed
    [SerializeField]
    private float gravityScale = 1.0f;
    [SerializeField]
    private float jumpAcceleration = 10.0f; // Adjust for smoother jumping

    private float gravity = -10f;

    private CharacterController characterController;

    private bool isGrounded; // Flag to track if the character is grounded
    private bool isJumping; // Flag to track if the character is currently jumping
    private float jumpVelocity; // Current jump velocity

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove) + (transform.forward * zMove);
        moveDirection.y += gravity * Time.deltaTime * gravityScale;

        // Check if the character is grounded
        isGrounded = characterController.isGrounded;

        // Apply movement
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump")) // Only jump if grounded and jump button is pressed
        {
            // Start jumping
            isJumping = true;
            jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); // Initial jump velocity
        }

        if (isJumping)
        {
            // Apply gravity
            jumpVelocity += gravity * jumpAcceleration * Time.deltaTime;

            // Check if character has reached peak jump height
            if (jumpVelocity < 0 && characterController.isGrounded)
            {
                // End jump
                isJumping = false;
            }
            else
            {
                // Apply jump velocity
                characterController.Move(Vector3.up * jumpVelocity * Time.deltaTime);
            }
        }
    }
}
