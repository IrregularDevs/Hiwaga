using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public Transform model; // Assign your 3D model (child of the player object)
    public float rotationSpeed = 10f;

    private CharacterController3D movementController;

    void Start()
    {
        movementController = GetComponent<CharacterController3D>();
    }

    void Update()
    {
        Vector3 moveDir = movementController.CurrentMoveDirection;

        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
