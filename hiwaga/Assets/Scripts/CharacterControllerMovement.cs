using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private float jumpAcceleration = 10.0f;

    private float gravity = -10f;
    private CharacterController characterController;

    public bool canMove = true; // Controls movement

    private bool isGrounded;
    private bool isJumping;
    private float jumpVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!canMove)
        {
            characterController.Move(Vector3.zero); // Completely stop movement
            return;
        }

        Move();
        Jump();
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove) + (transform.forward * zMove);
        moveDirection.y += gravity * Time.deltaTime * gravityScale;

        isGrounded = characterController.isGrounded;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isJumping)
        {
            jumpVelocity += gravity * jumpAcceleration * Time.deltaTime;

            if (jumpVelocity < 0 && characterController.isGrounded)
            {
                isJumping = false;
            }
            else
            {
                characterController.Move(Vector3.up * jumpVelocity * Time.deltaTime);
            }
        }
    }
}
