using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class AreaCameraTransition : MonoBehaviour
{
    public CinemachineCamera thirdPersonCam;
    public CinemachineCamera areaCam;
    public float transitionTime = 3.0f;
    public float cutsceneDuration = 5.0f; // Duration of the cutscene (adjust as needed)

    private CharacterController3D playerController;
    private bool hasTriggered = false; // Flag to ensure one-time execution

    private void Start()
    {
        playerController = FindFirstObjectByType<CharacterController3D>();

        // Ensure third-person camera starts active
        thirdPersonCam.Priority = 10;
        areaCam.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Set flag to prevent re-triggering
            StartCoroutine(TransitionToAreaView());
        }
    }

    private IEnumerator TransitionToAreaView()
    {
        if (playerController != null)
        {
            playerController.canMove = false; // Disable Movement
        }

        // Switch to area camera
        thirdPersonCam.Priority = 0;
        areaCam.Priority = 20;

        // Wait for cutscene duration
        yield return new WaitForSeconds(cutsceneDuration);

        // End cutscene and switch back to third-person camera
        areaCam.Priority = 0;
        thirdPersonCam.Priority = 10;

        if (playerController != null)
        {
            playerController.canMove = true; // Re-enable Movement
        }
    }
}
