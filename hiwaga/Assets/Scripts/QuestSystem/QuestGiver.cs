using UnityEngine;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public List<Quest> quest = new List<Quest>();
    public bool repeatable;
    public bool hasFinished;
    public int timesAccepted;
    public int maxAccepts;
    public bool autoGive;
    public bool isActive;

    public void OnQuestFinish()
    {
        timesAccepted++;
        hasFinished = true;
        isActive = false;
    }

    public void Start()
    {
        if(autoGive)
        {
            if (repeatable && timesAccepted < maxAccepts)
            {
                return;
            }
            if (!repeatable && hasFinished)
            {
                return;
            }
            foreach (Quest quest in quest)
            {
                QuestManager.Instance.AddQuest(quest);
            }
            timesAccepted++;
            isActive = true;
        }
    }

    public void Interact()
    {
        if(isActive)
        {
            return;
        }
        if(repeatable && timesAccepted < maxAccepts)
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
            quest.onQuestComplete += OnQuestFinish;
        }
        timesAccepted++;
        isActive = true;
    }

    public void enterPrompt()
    {
        Debug.Log("QuestGiver entered.");
    }

    public void exitPrompt()
    {
        Debug.Log("QuestGiver exited.");
    }

    public bool canInteract()
    {
        return true; // Always allow interaction
    }
}
