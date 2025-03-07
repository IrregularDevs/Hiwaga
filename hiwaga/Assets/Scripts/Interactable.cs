using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string enterString, exitString, interactString;
    [SerializeField]
    private List<DialogueManager> dialogueManagers; // List of DialogueManager
    [SerializeField]
    private TextMeshProUGUI dialogueText, nameText;
    [SerializeField]
    private GameObject panel;

    private int currentDialogueIndex = 0;

    public void enterPrompt()
    {
        Debug.Log($"Hi I'm {dialogueManagers[currentDialogueIndex].character_name} nice to meet you");
        //Show an indicator to the player that they can interact with the object
    }

    public void exitPrompt()
    {
        Debug.Log("Farewell");
        //remove said indicator from enterPrompt.
        panel.SetActive(false);
    }

    public void interactPrompt()
    {
        if (currentDialogueIndex < dialogueManagers.Count)
        {
            //Show the dialogue box
            panel.SetActive(true);
            //Show the name of the character
            nameText.text = dialogueManagers[currentDialogueIndex].character_name;
            Debug.Log(dialogueManagers[currentDialogueIndex].dialogue);
            //Make the UI Show the Text Corresponding to the character
            dialogueText.text = dialogueManagers[currentDialogueIndex].dialogue;

            // Move to the next dialogue
            currentDialogueIndex++;
        }
        else
        {
            // No more dialogues, hide the panel
            panel.SetActive(false);
            currentDialogueIndex = 0; // Reset the index if needed
        }
    }

    private void interaction()
    {
        //This is where the interaction happens
    }
}