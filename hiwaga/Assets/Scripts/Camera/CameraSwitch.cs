using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera thirdPersonCamera; // Reference to the third-person camera
    public CinemachineCamera alternateCamera; // Reference to the alternate camera
    private Collider triggerCollider; // Reference to the trigger collider

    private void Start()
    {
        // Make sure the cameras are set to the correct state at the start
        if (thirdPersonCamera != null && alternateCamera != null)
        {
            thirdPersonCamera.gameObject.SetActive(true);
            alternateCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the collider, switch to the alternate camera
        if (other.CompareTag("Player"))
        {
            SwitchCamera(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player exits the collider, switch back to the third-person camera
        if (other.CompareTag("Player"))
        {
            SwitchCamera(false);
        }
    }

    private void SwitchCamera(bool useAlternate)
    {
        // Enable/Disable the cameras based on the input parameter
        if (useAlternate)
        {
            if (thirdPersonCamera != null) thirdPersonCamera.gameObject.SetActive(false);
            if (alternateCamera != null) alternateCamera.gameObject.SetActive(true);
        }
        else
        {
            if (thirdPersonCamera != null) thirdPersonCamera.gameObject.SetActive(true);
            if (alternateCamera != null) alternateCamera.gameObject.SetActive(false);
        }
    }
}
