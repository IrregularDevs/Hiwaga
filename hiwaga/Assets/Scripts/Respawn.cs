using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float fallThreshold = -10f;
    public float respawnDelay = 1f;

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
        // Save only grounded positions (safer)
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

        transform.position = respawnPosition + Vector3.up * 0.2f;

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

    // ? USED BY LAVA TELEPORT
    public Vector3 GetLastSafePosition()
    {
        if (positionHistory.Count > 0)
            return positionHistory.Peek().position;

        return transform.position;
    }

    // Optional: prevent spam teleport loops
    public void ClearHistory()
    {
        positionHistory.Clear();
    }
}