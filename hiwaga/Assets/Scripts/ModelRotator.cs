using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public Transform model;
    public Transform cameraTransform;
    public float rotationSpeed = 10f;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (model == null || cameraTransform == null)
            return;

        // Only allow rotation in the Overworld state
        if (GameManager.currentGameState != GameState.Overworld)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(moveX, 0f, moveZ).normalized;

        // Only rotate while there is movement input
        if (input.sqrMagnitude > 0.01f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = (camForward * input.z + camRight * input.x).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            model.rotation = Quaternion.Slerp(
                model.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }
    }
}