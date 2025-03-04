using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Player to follow
    public Vector3 offset = new Vector3(0, 2, -5); // Fixed camera offset
    public float smoothSpeed = 10.0f; // Smooth movement speed

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate fixed position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position);
    }
}
