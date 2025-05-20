using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public Transform model; // Assign the model, e.g. metarig
    public float rotationSpeed = 10f;

    private CharacterController3D movementController;
    private Animator animator;

    void Start()
    {
        movementController = GetComponent<CharacterController3D>();
        animator = model.GetComponent<Animator>(); // Assumes Animator is on "metarig"
    }

    void Update()
    {
        Vector3 moveDir = movementController.CurrentMoveDirection;

        bool isWalking = moveDir.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

}
