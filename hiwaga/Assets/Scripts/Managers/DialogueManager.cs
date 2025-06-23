using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    public DialogueGroup[] dialogueGroups;
    public List<NPC> npcList = new List<NPC>();
    public DialogueGroup currentDialogueGroup;
    public DialogueData currentDialogueData;
    public NPCDialogue currentDialogue;
    public NPC currentNPC;

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

    public void BeginDialogue(NPC npc)
    {
        currentNPC = npc;
        currentDialogueGroup = npc.GetDialogueGroup();
        currentDialogueData = currentDialogueGroup.dialogue[npc.GetIndex()];
        currentDialogue = currentDialogueData.dialogue;

        if (currentDialogueGroup == null)
        {
            Debug.Log("CurrentDialogueGroup is empty.");
        }
        if (currentDialogueData == null)
        {
            Debug.Log("CurrentDialogueData is empty.");
        }
        if (currentDialogue == null)
        {
            Debug.Log("CurrentDialogueData is empty.");
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
        Debug.Log($"Player's name is {Player.Instance.playerName}.");
        nameText.text = currentDialogue.dialogueLines[0].character.npcName.Replace("&name", Player.Instance.playerName);
        portraitImage.sprite = currentDialogue.dialogueLines[0].character.npcPortrait;
        PauseManager.SetPause(true);
        StartCoroutine(TypeLine());
    }

    void nextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentDialogue.dialogueLines[dialogueIndex].dialogueLine.Replace("&name", Player.Instance.playerName);
            isTyping = false;
            return;
        }

        dialogueIndex++;
        if (dialogueIndex < currentDialogue.dialogueLines.Length)
        {
            Debug.Log($"Player's name is {Player.Instance.playerName}.");
            nameText.text = currentDialogue.dialogueLines[dialogueIndex].character.npcName.Replace("&name", Player.Instance.playerName);
            portraitImage.sprite = currentDialogue.dialogueLines[dialogueIndex].character.npcPortrait;
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
        Debug.Log($"Player's name is {Player.Instance.playerName}.");
        string line = currentDialogue.dialogueLines[dialogueIndex].dialogueLine.Replace("&name", Player.Instance.playerName);
        foreach (char letter in currentDialogue.dialogueLines[dialogueIndex].dialogueLine.Replace("&name", Player.Instance.playerName))
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }
        isTyping = false;
        if (currentDialogue.autoProgressLines.Length > dialogueIndex && currentDialogue.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(currentDialogue.autoProgressDelay);
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
        if(!currentDialogue.loops)
        {
            currentNPC.SetIndex(currentDialogueData.nextIndex);
        }
        if(currentDialogue.givesQuest)
        {
            Debug.Log("Quest given via Dialogue Manager.");
            QuestManager.Instance.AddQuest(currentDialogue.questToGive);
        }
    }

    public void ChangeIndex(DialogueGroup dGroup, int newIndex)
    {
        foreach(NPC npc in npcList)
        {
            if(npc.GetDialogueGroup() == dGroup)
            {
                npc.SetIndex(newIndex);
            }
        }
    }
}
