using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Player to follow
    public Vector3 defaultOffset = new Vector3(0, 2, -5); // Default third-person offset
    public Vector3 zoomedOutOffset = new Vector3(0, 5, -10); // Offset when zoomed out
    public float smoothSpeed = 5.0f; // Camera movement smoothness
    public float zoomSpeed = 2.0f; // Zoom transition speed
    public float collisionRadius = 0.3f; // Buffer to avoid clipping
    public LayerMask obstacleLayers; // Layers considered obstacles

    private Vector3 currentOffset;
    private bool isZoomedOut = false;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("⚠️ Target is not assigned to the camera!");
            enabled = false;
            return;
        }

        // Set default offset
        currentOffset = defaultOffset;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Smoothly transition between zoomed and default offsets
        Vector3 desiredOffset = isZoomedOut ? zoomedOutOffset : defaultOffset;
        currentOffset = Vector3.Lerp(currentOffset, desiredOffset, Time.deltaTime * zoomSpeed);

        // Calculate camera's desired position
        Vector3 desiredPosition = target.position + currentOffset;

        // Check for obstacles between the player and the camera
        if (Physics.Raycast(target.position, (desiredPosition - target.position).normalized, out RaycastHit hit, currentOffset.magnitude, obstacleLayers))
        {
            // Move camera closer to avoid clipping into obstacles
            desiredPosition = hit.point + (target.position - hit.point).normalized * collisionRadius;
        }

        // Smoothly move the camera to the new position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // 🔹 Maintain a fixed rotation instead of looking at the player
        transform.rotation = Quaternion.Euler(20, 0, 0); // Adjust the angle as needed
    }

    // 📌 Public Methods for External Zoom Control
    public void ZoomOut() => isZoomedOut = true;
    public void ResetCamera() => isZoomedOut = false;
}
