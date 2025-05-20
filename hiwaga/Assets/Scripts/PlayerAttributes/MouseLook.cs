using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody; // Assign your Player GameObject
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock and hide the cursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Only rotate the player on the Y-axis
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
