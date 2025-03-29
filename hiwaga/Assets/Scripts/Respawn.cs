using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float fallThreshold = -10f; // The height at which the player will respawn
    private Vector3 respawnPoint; // Stores the starting position

    void Start()
    {
        respawnPoint = transform.position; // Set initial spawn point
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reset position
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // Disable controller to manually set position
            transform.position = respawnPoint;
            controller.enabled = true; // Re-enable controller
        }
        else
        {
            transform.position = respawnPoint;
        }
    }
}
