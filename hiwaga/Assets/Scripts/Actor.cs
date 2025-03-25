using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Actor : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private DialogueManager dialogueManager; // Single DialogueManager containing a list of dialogues
    [SerializeField] private TextMeshProUGUI dialogueText, nameText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image portrait;
    private AudioSource audioSource;

    private int currentDialogueIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void enterPrompt()
    {
        Debug.Log($"Hi I'm {dialogueManager.character_name} nice to meet you");
        //Show an indicator to the player that they can interact with the object
    }

    public void exitPrompt()
    {
        Debug.Log("Farewell");
        //remove said indicator from enterPrompt.
        panel.SetActive(false);
    }

    public void Interact()
    {
        if (currentDialogueIndex < dialogueManager.dialogues.Count)
        {
            audioSource.Play();
            //Show the dialogue box
            panel.SetActive(true);
           portrait.sprite = dialogueManager.portrait;
            //Show the name of the character
            nameText.text = dialogueManager.character_name;
            Debug.Log(dialogueManager.dialogues[currentDialogueIndex]);
            //Make the UI Show the Text Corresponding to the character
            dialogueText.text = dialogueManager.dialogues[currentDialogueIndex];

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
}