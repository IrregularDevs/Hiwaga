#nullable enable
using UnityEngine;

public class Lock : Interactable
{
    [SerializeField] protected Item? requiredItem;

    public delegate void OnInteract();
    public OnInteract? onInteract;

    public override void SetActivePrompt(bool state)
    {
        if(Player.Instance.HasItem(requiredItem))
        {
            base.SetActivePrompt(state);
        }
    }

    public override void Interact()
    {
        if (Player.Instance.HasItem(requiredItem))
        {
            onInteract?.Invoke();
        }
    }
}
