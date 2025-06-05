using UnityEngine;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public List<Quest> quest = new List<Quest>();
    public bool repeatable;
    public bool hasFinished;
    public int timesCompleted;
    public int maxCompletes;

    public void Interact()
    {
        if(repeatable && timesCompleted < maxCompletes)
        {
            return;
        }
        if(!repeatable && hasFinished)
        {
            return;
        }
        foreach (Quest quest in quest)
        {
            QuestManager.Instance.AddQuest(quest);
        }
        timesCompleted++;
        hasFinished = true;
    }

    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }
    public bool canInteract()
    {
        return true; // Always allow interaction
    }
}
