using System.Collections;
using UnityEngine;

public class AreaCameraTransition : MonoBehaviour
{
    public ThirdPersonCamera mainCameraScript; // Reference to the third-person camera script
    public Camera mainCamera; // Reference to the main third-person camera
    public Camera areaCamera; // Reference to the pre-existing area camera
    public float transitionDuration = 2f; // Duration of the camera transition
    public GameObject player; // Reference to the player
    private bool hasEntered = false;
    private CharacterControllerMovement playerController; // Reference to player movement script

    void Start()
    {
        areaCamera.gameObject.SetActive(false); // Ensure the area camera is initially disabled
        playerController = player.GetComponent<CharacterControllerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !hasEntered)
        {
            hasEntered = true;
            StartCoroutine(SwitchToAreaCamera());
        }
    }

    IEnumerator SwitchToAreaCamera()
    {
        float elapsedTime = 0f;
        mainCameraScript.enabled = false; // Disable third-person camera movement
        if (playerController != null)
        {
            playerController.enabled = false; // Disable player movement
        }
        areaCamera.gameObject.SetActive(true);

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, areaCamera.transform.position, t);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, areaCamera.transform.rotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.enabled = false;
        areaCamera.enabled = true;

        yield return new WaitForSeconds(transitionDuration);

        StartCoroutine(SwitchBackToThirdPersonCamera());
    }

    IEnumerator SwitchBackToThirdPersonCamera()
    {
        float elapsedTime = 0f;
        areaCamera.enabled = true;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            areaCamera.transform.position = Vector3.Lerp(areaCamera.transform.position, mainCamera.transform.position, t);
            areaCamera.transform.rotation = Quaternion.Slerp(areaCamera.transform.rotation, mainCamera.transform.rotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        areaCamera.enabled = false;
        areaCamera.gameObject.SetActive(false);
        mainCamera.enabled = true;
        mainCameraScript.enabled = true; // Re-enable third-person camera movement
        if (playerController != null)
        {
            playerController.enabled = true; // Re-enable player movement
        }
    }
}
