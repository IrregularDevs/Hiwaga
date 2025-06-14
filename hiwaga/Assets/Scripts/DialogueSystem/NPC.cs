using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DialogueData
{
    public NPCDialogue dialogue;
    public int count;
}

public class NPC : MonoBehaviour, IInteractable
{
    public List<DialogueData> dialogueData = new List<DialogueData>();
    public string npcName;

    
    public bool canInteract()
    {
        return !DialogueManager.Instance.isdialogueActive;
    }

    public void Interact()
    {
        DialogueManager.Instance.BeginDialogue(this);
    }



    public void enterPrompt()
    {
        // Implement enter prompt logic here
        // Example: Debug.Log("Player entered NPC interaction range.");
    }

    public void exitPrompt()
    {
        // Implement exit prompt logic here
        // Example: Debug.Log("Player exited NPC interaction range.");
    }
}
