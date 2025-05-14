using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public Quest quest;

    public void Interact()
    {
        Debug.Log("QuestGiver used.");
        QuestManager.Instance.AddQuest(quest);
    }

    public void enterPrompt()
    {
        Debug.Log("QuestGiver entered.");
    }

    public void exitPrompt()
    {
        Debug.Log("QuestGiver exited.");
    }
}
