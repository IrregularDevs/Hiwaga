using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float fallThreshold = -10f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
        Transform nearestRespawn = FindNearestRespawn(transform.position);

        if (nearestRespawn == null)
        {
            Debug.LogWarning("No RespawnPoint objects found!");
            return;
        }

        if (controller != null)
        {
            controller.enabled = false;
            transform.position = nearestRespawn.position;
            controller.enabled = true;
        }
        else
        {
            transform.position = nearestRespawn.position;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    Transform FindNearestRespawn(Vector3 playerPosition)
    {
        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");

        Transform nearest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject point in respawnPoints)
        {
            float distance = Vector3.Distance(playerPosition, point.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearest = point.transform;
            }
        }

        return nearest;
    }
}