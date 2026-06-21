using System.Collections;
using UnityEngine;

public class FishRide : Interactable
{
    [Header("Ride Settings")]
    [SerializeField] private Transform seatPoint;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float mountDelay = 1f;

    private bool isMoving;
    private bool playerInside;
    private bool rideCompleted;

    private void Update()
    {
        if (playerInside && !isMoving && !rideCompleted && Input.GetKeyDown(KeyCode.E))
        {
            playerInside = false;
            SetActivePrompt(false);
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !rideCompleted)
        {
            playerInside = true;
            SetActivePrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            SetActivePrompt(false);
        }
    }

    public override void Interact()
    {
        if (isMoving || rideCompleted)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No Player object found! Make sure the player has the Player tag.");
            return;
        }

        StartCoroutine(RideFish(player));
    }

    private IEnumerator RideFish(GameObject player)
    {
        isMoving = true;

        SetActivePrompt(false);

        CharacterController3D playerMovement =
            player.GetComponent<CharacterController3D>();

        CharacterController characterController =
            player.GetComponent<CharacterController>();

        // Disable movement and jump
        if (playerMovement != null)
        {
            playerMovement.canMove = false;
            playerMovement.enableJump = false;
        }

        // Disable CharacterController before teleporting
        if (characterController != null)
            characterController.enabled = false;

        // Move player to seat
        player.transform.position = seatPoint.position;
        player.transform.rotation = seatPoint.rotation;

        // Parent player to fish
        player.transform.SetParent(transform, true);

        yield return new WaitForSeconds(mountDelay);

        // Move fish
        while (Vector3.Distance(transform.position, pointB.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pointB.position,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = pointB.position;

        // Ride finished
        rideCompleted = true;
        playerInside = false;

        SetActivePrompt(false);

        // Unparent player
        player.transform.SetParent(null, true);

        // Re-enable CharacterController
        if (characterController != null)
            characterController.enabled = true;

        // Re-enable movement and jump
        if (playerMovement != null)
        {
            playerMovement.canMove = true;
            playerMovement.enableJump = true;
        }

        // Disable trigger so ride cannot be used again
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        if (seatPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(seatPoint.position, 0.2f);
        }

        if (pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointB.position, 0.2f);
        }
    }
}