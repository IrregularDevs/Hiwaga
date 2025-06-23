using UnityEngine;

public class HopkinsNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private FindQuest findQuest;
    [SerializeField] private FindQuestObjective objectiveToHide;
    private bool playerInRange = false;

    public void Interact()
    {
        if (!canInteract()) return;
    }

    public void enterPrompt() => playerInRange = true;
    public void exitPrompt() => playerInRange = false;
    public bool canInteract() => playerInRange;
}
