using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour
{
    public Camera MainCamera; // Main player camera
    public Camera cutsceneCamera; // Cutscene camera
    public KeyCode interactKey = KeyCode.E; // Key to start cutscene
    public KeyCode skipKey = KeyCode.Space; // Key to skip cutscene
    public GameObject player; // Player reference
    public float transitionSpeed = 2f; // Speed of transition

    private bool isCutsceneActive = false;
    private CharacterController3D playerController; // movement script

    void Start()
    {
        if (player != null)
        {
            playerController = player.GetComponent<CharacterController3D>(); 
        }

        // Ensure only the player camera is active at the start
        MainCamera.gameObject.SetActive(true);
        cutsceneCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && !isCutsceneActive)
        {
            StartCoroutine(StartCutscene());
        }

        if (isCutsceneActive && Input.GetKeyDown(skipKey))
        {
            StartCoroutine(EndCutscene());
        }
    }

    IEnumerator StartCutscene()
    {
        isCutsceneActive = true;

        // Disable player movement
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Activate cutscene camera and smoothly transition
        cutsceneCamera.gameObject.SetActive(true);
        yield return StartCoroutine(SmoothCameraTransition(MainCamera, cutsceneCamera));

        // Wait until the player presses space
        yield return new WaitUntil(() => Input.GetKeyDown(skipKey));

        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene()
    {
        // Smoothly transition back to the player camera
        yield return StartCoroutine(SmoothCameraTransition(cutsceneCamera, MainCamera));

        // Disable cutscene camera and re-enable player movement
        cutsceneCamera.gameObject.SetActive(false);
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        isCutsceneActive = false;
    }

    IEnumerator SmoothCameraTransition(Camera fromCamera, Camera toCamera)
    {
        float t = 0f;
        Vector3 startPosition = fromCamera.transform.position;
        Quaternion startRotation = fromCamera.transform.rotation;
        float startFOV = fromCamera.fieldOfView;

        Vector3 targetPosition = toCamera.transform.position;
        Quaternion targetRotation = toCamera.transform.rotation;
        float targetFOV = toCamera.fieldOfView;

        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            fromCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            fromCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            fromCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        // Ensure final values match
        fromCamera.transform.position = targetPosition;
        fromCamera.transform.rotation = targetRotation;
        fromCamera.fieldOfView = targetFOV;

        // Disable the starting camera when transition completes
        fromCamera.gameObject.SetActive(false);
        toCamera.gameObject.SetActive(true);
    }
}
