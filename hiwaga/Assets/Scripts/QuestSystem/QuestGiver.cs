using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public Quest quest;

    public void Interact()
    {
        QuestManager.Instance.AddQuest(quest);
    }

    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }
}
