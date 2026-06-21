using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Singleton Implementation
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    //Lists and Dictionaries
    public List<NPCDialogue> npcDialogue_List = new List<NPCDialogue>();
    public List<NPC> npc_List = new List<NPC>();
    public Dictionary<NPC, int> m_npcDictionary = new Dictionary<NPC, int>();
    public Dictionary<(NPC npc, int dialogueIndex), NPCDialogue> m_dialogueDictionary = new Dictionary<(NPC npc, int dialogueIndex), NPCDialogue>();

    //Dialogue reference data
    public NPC currentNPC;
    public NPCDialogue currentDialogue;

    //UI
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    //Dialogue tracking data
    public int dialogueLineIndex;
    public int dialogueDataIndex;
    public bool isdialogueActive, isTyping;

    //Set Singleton
    private void Awake()
    {
        instance = this;
    }

    //Remove Singleton
    private void OnDisable()
    {
        instance = null;
    }

    //Connect all NPCs and NPCDialogues in level
    private void Start()
    {
        foreach (NPC usedNpc in npc_List)
        {
            List<NPCDialogue> validDialogue = new List<NPCDialogue>();
            validDialogue = npcDialogue_List.FindAll(x => x.npcRefName == usedNpc.GetRefName());

            foreach (NPCDialogue dialogueToBeAdded in validDialogue)
            {
                m_dialogueDictionary.Add((usedNpc, dialogueToBeAdded.dialogueIndex), dialogueToBeAdded);
            }
            m_npcDictionary.Add(usedNpc, usedNpc.GetIndex());
        }
        dialoguePanel.SetActive(false);
    }

    //Called when NPC is interacted with
    //Start dialogue or go to next line
    public void BeginDialogue(NPC npc)
    {
        currentNPC = npc;
        if(m_dialogueDictionary.Count != 0)
        {
            currentDialogue = m_dialogueDictionary[(currentNPC, m_npcDictionary[currentNPC])];
        }
        else
        {
            Debug.Log("Huh.");
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

    //Called when new dialogue starts
    //Prepare dialogue UI
    void StartDialogue()
    {
        isdialogueActive = true;
        dialogueLineIndex = 0;

        dialoguePanel.SetActive(true);
        nameText.text = currentDialogue.dialogueLines[0].character.npcName.Replace("&name", Player.Instance.playerName);
        portraitImage.sprite = currentDialogue.dialogueLines[0].character.npcPortrait;

        PauseManager.SetPause(true);
        StartCoroutine(TypeLine());
    }

    //Called when existing dialogue progresses
    //Prepare dialogue UI
    void nextLine()
    {
        StopAllCoroutines();
        if (isTyping)
        {
            dialogueText.text = currentDialogue.dialogueLines[dialogueLineIndex].dialogueLine.Replace("&name", Player.Instance.playerName);
            isTyping = false;
            StartCoroutine(NextLine());
            return;
        }

        dialogueLineIndex++;
        if (dialogueLineIndex < currentDialogue.dialogueLines.Length)
        {
            nameText.text = currentDialogue.dialogueLines[dialogueLineIndex].character.npcName.Replace("&name", Player.Instance.playerName);
            portraitImage.sprite = currentDialogue.dialogueLines[dialogueLineIndex].character.npcPortrait;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    //Called when dialogue starts
    //Type out spoken line
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        string line = currentDialogue.dialogueLines[dialogueLineIndex].dialogueLine.Replace("&name", Player.Instance.playerName);

        foreach (char letter in currentDialogue.dialogueLines[dialogueLineIndex].dialogueLine.Replace("&name", Player.Instance.playerName))
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentDialogue.dialogueLines[dialogueLineIndex].typingSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(currentDialogue.dialogueLines[dialogueLineIndex].autoProgressDelayFast);
        nextLine();
    }

    //Called when dialogue ends
    //Progress dialogue
    public void EndDialogue()
    {
        StopAllCoroutines();
        isdialogueActive = false;
        dialoguePanel.SetActive(false);
        PauseManager.SetPause(false);
        dialogueLineIndex = 0;

        if(!currentDialogue.loops)
        {
            m_npcDictionary[currentNPC] = m_npcDictionary[currentNPC] + 1;
            /*ChangeIndex(currentDialogueGroup, currentDialogueData.nextIndex);
            if(currentDialogue.questToGive != null)
            {
                QuestManager.Instance.AddQuest(currentDialogue.questToGive);
            }*/
        }
    }

    /*public void ChangeIndex(DialogueGroup dGroup, int newIndex)
    {
        foreach(NPC npc in npc_List)
        {
            if(npc.GetDialogueGroup() == dGroup)
            {
                npc.SetIndex(newIndex);
            }
        }
    }*/

    //Called when player interacts with NPC while dialogue is being typed
    //Skips to next line
    public IEnumerator NextLine()
    {
        yield return new WaitForSeconds(currentDialogue.dialogueLines[dialogueLineIndex].autoProgressDelaySlow);
        nextLine();
    }
}
