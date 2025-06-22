using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueGroup npcDialogue;
    [SerializeField] private int index;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        DialogueManager.Instance.npcList.Add(this);
        yield return null;
    }

    public bool canInteract()
    {
        return !DialogueManager.Instance.isdialogueActive;
    }

    public void Interact()
    {
        DialogueManager.Instance.BeginDialogue(this);
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int i)
    {
        Debug.Log("Index changed");
        index = i;
    }

    public DialogueGroup GetDialogueGroup()
    {
        return npcDialogue;
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
