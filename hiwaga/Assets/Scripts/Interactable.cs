using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string enterString, exitString, interactString;
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private TextMeshProUGUI dialogueText, nameText;
    [SerializeField]
    private GameObject panel;

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

    public void interactPrompt()
    {
        //Show the dialogue box
        panel.SetActive(true);
        //Show the name of the character
        nameText.text = dialogueManager.character_name;
        Debug.Log(dialogueManager.dialogue);
        //Make the UI Show the Text Corresponding to the character
        dialogueText.text = dialogueManager.dialogue;


    }
}
