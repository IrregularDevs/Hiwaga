using UnityEngine;

public class CameraLookAtPlayer : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public float smoothSpeed = 5f; // Smooth rotation speed

    void Update()
    {
        if (player == null) return;

        // Rotate the camera to always look at the player smoothly
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
