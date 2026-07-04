using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float fallThreshold = -10f;
    public float respawnDelay = 1f;
    public float respawnHeightOffset = 0.2f;

    private CharacterController controller;

    private struct PositionRecord
    {
        public Vector3 position;
        public float time;
    }

    private readonly Queue<PositionRecord> positionHistory = new Queue<PositionRecord>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Only save positions while the player is grounded
        if (controller != null && controller.isGrounded)
        {
            positionHistory.Enqueue(new PositionRecord
            {
                position = transform.position,
                time = Time.time
            });

            while (positionHistory.Count > 0 &&
                   Time.time - positionHistory.Peek().time > respawnDelay)
            {
                positionHistory.Dequeue();
            }
        }

        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        Vector3 respawnPosition = transform.position;

        if (positionHistory.Count > 0)
        {
            respawnPosition = positionHistory.Peek().position;
        }

        if (controller != null)
            controller.enabled = false;

        // Spawn slightly above the saved grounded position
        transform.position = respawnPosition + Vector3.up * respawnHeightOffset;

        if (controller != null)
            controller.enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        positionHistory.Clear();
    }
}