using UnityEngine;

public class CameraZoneTrigger : MonoBehaviour
{
    public ThirdPersonCamera cameraScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cameraScript != null)
        {
            cameraScript.ZoomOut();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && cameraScript != null)
        {
            cameraScript.ResetCamera();
        }
    }
}
