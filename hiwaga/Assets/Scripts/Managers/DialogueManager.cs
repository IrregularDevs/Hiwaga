using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;
    public List<DialogueGroup> dialogueGroup = new List<DialogueGroup>();
    public DialogueGroup currentDialogueGroup;
    public NPCDialogue currentDialogue;

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public int dialogueIndex;
    public int dialogueDataIndex;
    public bool isdialogueActive, isTyping;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }

    public void Start()
    {
        foreach(DialogueGroup group in dialogueGroup)
        {
            group.currentIndex = 0;
        }
    }

    public void BeginDialogue(NPC npc)
    {
        currentDialogueGroup = dialogueGroup.Find(x => x.npcName == npc.npcName);
        if(currentDialogueGroup == null)
        {
            Debug.Log("CurrentDialogueGroup is empty.");
        }
        currentDialogue = currentDialogueGroup.dialogue.Find(x => x.count == currentDialogueGroup.currentIndex).dialogue;
        if (currentDialogue == null)
        {
            return;
        }
        if (isdialogueActive)
        {
            nextLine(npc);
        }
        else
        {
            StartDialogue(npc);
        }
    }

    void StartDialogue(NPC npc)
    {
        isdialogueActive = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        nameText.text = currentDialogue.npcName[0];
        portraitImage.sprite = currentDialogue.npcPortrait[0];
        PauseManager.SetPause(true);
        StartCoroutine(TypeLine(npc));
    }

    void nextLine(NPC npc)
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentDialogue.dialogueLines[dialogueIndex];
            isTyping = false;
            return;
        }

        dialogueIndex++;
        if (dialogueIndex < currentDialogue.dialogueLines.Length)
        {
            if (currentDialogue.dialogueSwitch[dialogueIndex])
            {
                nameText.text = currentDialogue.npcName[1];
                portraitImage.sprite = currentDialogue.npcPortrait[1];
            }
            else
            {
                nameText.text = currentDialogue.npcName[0];
                portraitImage.sprite = currentDialogue.npcPortrait[0];
            }
            StartCoroutine(TypeLine(npc));
        }
        else
        {
            EndDialogue(npc);
        }
    }

    IEnumerator TypeLine(NPC npc)
    {
        isTyping = true;
        dialogueText.text = "";
        string line = currentDialogue.dialogueLines[dialogueIndex];
        foreach (char letter in currentDialogue.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }
        isTyping = false;
        if (currentDialogue.autoProgressLines.Length > dialogueIndex && currentDialogue.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(currentDialogue.autoProgressDelay);
            nextLine(npc);
        }
    }


    public void EndDialogue(NPC npc)
    {
        StopAllCoroutines();
        isdialogueActive = false;
        dialoguePanel.SetActive(false);
        PauseManager.SetPause(false);
        dialogueIndex = 0;
    }

    public void ChangeIndex(string name, int i)
    {
        dialogueGroup.Find(x => x.npcName == name).ChangeIndex(i);
    }
}
