using UnityEngine;

public class Door_Benito : Lock
{
    [SerializeField] private ItemReceiver itemReceiver;

    private void Start()
    {
        onInteract += OpenDoor;
    }

    private void OnDisable()
    {
        onInteract -= OpenDoor;
    }

    private void OpenDoor()
    {
        InteractionManager.Instance.RemoveInteractTarget(this);
        itemReceiver.Interact();
        onInteract -= OpenDoor;
        gameObject.SetActive(false);
    }
}
