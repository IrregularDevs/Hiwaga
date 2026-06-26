using UnityEngine;

public class DialogueTrigger : NPC
{
    private int currentInteractions = 0;
    [SerializeField] private int maxInteractions;

    public override void Interact()
    {
        if(currentInteractions < maxInteractions)
        {
            currentInteractions++;
            base.Interact();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(maxInteractions < 0)
        {
            Interact();
        }
        if(currentInteractions < maxInteractions)
        {
            Interact();
        }
    }

    public override void SetActivePrompt(bool state)
    {

    }
}
