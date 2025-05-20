using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;       // Player transform (root, parent of model)
    public float rotationSpeed = 5f;

    [Tooltip("Height of the camera relative to the player")]
    public float height = 2f;

    [Tooltip("Distance behind the player")]
    public float distance = 4f;

    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw = 0f;
    private float pitch = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rotate player (body) on Y axis only by yaw
        Vector3 euler = target.eulerAngles;
        euler.y = yaw;
        target.eulerAngles = euler;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation * new Vector3(0, height, -distance);
        transform.position = target.position + offset;

        transform.LookAt(target.position + Vector3.up * height);
    }
}
