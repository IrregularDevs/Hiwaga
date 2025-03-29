using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Player to follow
    public Vector3 defaultOffset = new Vector3(0, 2, -5); // Default third-person offset
    public Vector3 zoomedOutOffset = new Vector3(0, 5, -10); // Offset when zoomed out
    public float smoothSpeed = 5.0f; // Camera movement smoothness
    public float zoomSpeed = 2.0f; // Zoom transition speed
    public float collisionRadius = 0.3f; // Small buffer to avoid clipping
    public LayerMask obstacleLayers; // Layers considered as obstacles

    private Vector3 currentOffset;
    private bool isZoomedOut = false;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned to the camera!");
            return;
        }

        // Set default offset
        currentOffset = defaultOffset;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Adjust zoom level smoothly
        Vector3 desiredOffset = isZoomedOut ? zoomedOutOffset : defaultOffset;
        currentOffset = Vector3.Lerp(currentOffset, desiredOffset, Time.deltaTime * zoomSpeed);

        // Calculate desired camera position
        Vector3 desiredPosition = target.position + currentOffset;

        // Check for obstacles between the player and camera
        RaycastHit hit;
        if (Physics.Raycast(target.position, (desiredPosition - target.position).normalized, out hit, currentOffset.magnitude, obstacleLayers))
        {
            // Move camera closer to avoid clipping into obstacles
            desiredPosition = hit.point + (target.position - hit.point).normalized * collisionRadius;
        }

        // Smoothly move the camera to the new position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // Look at the player
        transform.LookAt(target.position);
    }

    // Zoom out when entering the trigger zone
    public void ZoomOut()
    {
        isZoomedOut = true;
    }

    // Reset to normal view when leaving the trigger zone
    public void ResetCamera()
    {
        isZoomedOut = false;
    }
}
