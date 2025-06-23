using UnityEngine;

public class HopkinsNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Quest questToGive;
    [SerializeField] private FindObjective objectiveToHide;
    private bool playerInRange = false;

    public void Interact()
    {
        if (!canInteract()) return;

        if (!QuestManager.Instance.HasQuest(questToGive))
        {
            QuestManager.Instance.AddQuest(questToGive);
            Debug.Log("Hopkins: A lucky guess! But you won't find me this time!");

            if (objectiveToHide != null)
            {
                objectiveToHide.HideInRandomSpot(); // Make him hide
            }
        }
        else
        {
            Debug.Log("Hopkins: You're already on this quest.");
        }
    }

    public void enterPrompt() => playerInRange = true;
    public void exitPrompt() => playerInRange = false;
    public bool canInteract() => playerInRange;
}
