using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    public bool isdialogueActive, isTyping;
    
    public bool canInteract()
    {
        return !isdialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || !isdialogueActive)
        {
            return;
        }
        if (isdialogueActive)
        {
            nextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isdialogueActive = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        nameText.text = dialogueData.npcName;
        portraitImage.sprite = dialogueData.npcPortrait;
        PauseManager.SetPause(true);
        StartCoroutine(TypeLine());
    }

    void nextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.dialogueLines[dialogueIndex];
            isTyping = false;
            return;
        }

        dialogueIndex++;
        if (dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        string line = dialogueData.dialogueLines[dialogueIndex];
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }
        isTyping = false;
        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            nextLine();
        }
    }


    public void EndDialogue()
    {
        StopAllCoroutines();
        isdialogueActive = false;
        dialoguePanel.SetActive(false);
        PauseManager.SetPause(false);
        dialogueIndex = 0;
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
