using System.Collections;
using UnityEngine;

public class TeleportToSafePosition : MonoBehaviour
{
    public float movementLockTime = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        StartCoroutine(TeleportPlayer(other));
    }

    private IEnumerator TeleportPlayer(Collider other)
    {
        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
        CharacterController3D movement = other.GetComponent<CharacterController3D>();
        CharacterController controller = other.GetComponent<CharacterController>();

        if (respawn == null)
            yield break;

        // Lock movement
        if (movement != null)
            movement.canMove = false;

        if (controller != null)
            controller.enabled = false;

        // Teleport to last safe position
        Vector3 safePos = respawn.GetLastSafePosition();

        other.transform.position = safePos + Vector3.up * 0.2f;

        // Reset physics
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (controller != null)
            controller.enabled = true;

        // Prevent instant re-trigger spam
        respawn.ClearHistory();

        yield return new WaitForSeconds(movementLockTime);

        if (movement != null)
            movement.canMove = true;
    }
}