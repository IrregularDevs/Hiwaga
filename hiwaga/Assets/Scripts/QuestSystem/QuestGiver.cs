using UnityEngine;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public List<Quest> quest = new List<Quest>();

    public void Interact()
    {
        foreach(Quest quest in quest)
        {
            QuestManager.Instance.AddQuest(quest);
        }
    }

    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }
}
