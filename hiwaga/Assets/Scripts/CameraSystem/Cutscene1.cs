using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour
{
    public Camera MainCamera; // Main player camera
    public Camera cutsceneCamera; // Cutscene camera
    public KeyCode interactKey = KeyCode.E; // Key to start cutscene
    public KeyCode skipKey = KeyCode.Space; // Key to skip cutscene
    public GameObject player; // Player reference
    public float interactionDistance = 3f; // Required distance to interact
    public float transitionSpeed = 2f; // Speed of transition

    private bool isCutsceneActive = false;
    private CharacterController3D playerController; // Movement script

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
        if (Vector3.Distance(player.transform.position, transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(interactKey) && !isCutsceneActive)
            {
                StartCoroutine(StartCutscene());
            }
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

        // Smooth transition to cutscene camera
        yield return StartCoroutine(SmoothCameraTransition(MainCamera, cutsceneCamera));

        // Wait until the player presses space
        yield return new WaitUntil(() => Input.GetKeyDown(skipKey));

        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene()
    {
        // Smooth transition back to the player camera
        yield return StartCoroutine(SmoothCameraTransition(cutsceneCamera, MainCamera));

        // Enable player movement
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        isCutsceneActive = false;
    }

    IEnumerator SmoothCameraTransition(Camera fromCamera, Camera toCamera)
    {
        float t = 0f;
        float duration = 1f / transitionSpeed; // Smooth transition duration

        Vector3 startPosition = fromCamera.transform.position;
        Quaternion startRotation = fromCamera.transform.rotation;
        float startFOV = fromCamera.fieldOfView;

        Vector3 targetPosition = toCamera.transform.position;
        Quaternion targetRotation = toCamera.transform.rotation;
        float targetFOV = toCamera.fieldOfView;

        toCamera.gameObject.SetActive(true);

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
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
    }
}
