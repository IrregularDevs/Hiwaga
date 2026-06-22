using UnityEngine;

public class DialogueTrigger : NPC
{
    private int currentInteractions;
    [SerializeField] private int maxInteractions;
    public override void Interact()
    {
        Debug.Log("Dialogue trigger.");
        base.Interact();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(maxInteractions <= 0)
        {
            Interact();
        }
        if(currentInteractions < maxInteractions)
        {
            Interact();
            currentInteractions++;
        }
    }

    public override void SetActivePrompt(bool state)
    {

    }
}
