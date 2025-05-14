using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public Transform targetRoot; // The root GameObject that holds movement (usually the player)
    public float turnSpeed = 10f;

    private Vector3 lastPosition;

    void Start()
    {
        if (targetRoot == null)
            targetRoot = transform.parent;

        lastPosition = targetRoot.position;
    }

    void Update()
    {
        Vector3 movementDirection = targetRoot.position - lastPosition;

        if (movementDirection.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        lastPosition = targetRoot.position;
    }
}
