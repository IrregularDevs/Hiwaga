using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraV4 : MonoBehaviour
{
    public CinemachineThirdPersonFollow thirdPersonFollow;
    public InputActionReference lookInput; // Must be Vector2

    public float recenterDelay = 2f;
    public float recenterSpeed = 1.5f;

    private float idleTimer;
    private bool isOrbiting;

    void Update()
    {
        Vector2 input = lookInput.action.ReadValue<Vector2>();

        if (input.sqrMagnitude > 0.01f)
        {
            isOrbiting = true;
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > recenterDelay)
                isOrbiting = false;
        }

        if (!isOrbiting)
        {
            float currentYaw = thirdPersonFollow.CameraSide;
            thirdPersonFollow.CameraSide = Mathf.Lerp(currentYaw, 0f, Time.deltaTime * recenterSpeed);
        }
    }
}
