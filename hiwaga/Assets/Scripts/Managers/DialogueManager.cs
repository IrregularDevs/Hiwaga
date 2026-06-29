using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    //Singleton Implementation
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    //Lists and Dictionaries
    public List<NPCDialogue> npcDialogue_List = new List<NPCDialogue>();
    public List<NPC> npc_List = new List<NPC>();
    public Dictionary<string, int> m_npcDictionary = new Dictionary<string, int>();
    public Dictionary<(string npc, int dialogueIndex), NPCDialogue> m_dialogueDictionary = new Dictionary<(string npc, int dialogueIndex), NPCDialogue>();

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
        if(instance == null)
        {
            Debug.Log("Instance is null");
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        else if(instance != null && instance != this)
        {
            Debug.Log("Instance is not null");
            Destroy(this);
            return;
        }
    }

    //Connect all NPCs and NPCDialogues in level
    private void Start()
    {
        /*foreach (NPC usedNpc in npc_List)
        {
            List<NPCDialogue> validDialogue = new List<NPCDialogue>();
            validDialogue = npcDialogue_List.FindAll(x => x.npcRefName == usedNpc.GetRefName());

            foreach (NPCDialogue dialogueToBeAdded in validDialogue)
            {
                m_dialogueDictionary.Add((usedNpc, dialogueToBeAdded.dialogueIndex), dialogueToBeAdded);
            }
            m_npcDictionary.Add(usedNpc, usedNpc.GetIndex());
        }*/
        dialoguePanel.SetActive(false);
        SaveData saveData = LoadSystem.LoadGameData();
        if(saveData != null)
        {

        }
    }

    public void AddNPC(NPC newNPC)
    {
        npc_List.Add(newNPC);
        List<NPCDialogue> validDialogue = new List<NPCDialogue>();
        validDialogue = npcDialogue_List.FindAll(x => x.npcRefName == newNPC.GetRefName());
        foreach (NPCDialogue dialogueToBeAdded in validDialogue)
        {
            if(!m_dialogueDictionary.ContainsKey((newNPC.GetRefName(), dialogueToBeAdded.dialogueIndex)))
            {
                m_dialogueDictionary.Add((newNPC.GetRefName(), dialogueToBeAdded.dialogueIndex), dialogueToBeAdded);
            }
        }
        if (!m_npcDictionary.ContainsKey(newNPC.GetRefName()))
        {
            m_npcDictionary.Add(newNPC.GetRefName(), newNPC.GetIndex());
        }
    }

    //Called when NPC is interacted with
    //Start dialogue or go to next line
    public void BeginDialogue(NPC npc)
    {
        currentNPC = npc;
        if(m_dialogueDictionary.Count != 0)
        {
            Debug.Log($"{npc.GetRefName()} is now speaking {m_npcDictionary[currentNPC.GetRefName()]}");
            currentDialogue = m_dialogueDictionary[(currentNPC.GetRefName(), m_npcDictionary[currentNPC.GetRefName()])];
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
            m_npcDictionary[currentNPC.GetRefName()] = m_npcDictionary[currentNPC.GetRefName()] + 1;
            /*ChangeIndex(currentDialogueGroup, currentDialogueData.nextIndex);
            if(currentDialogue.questToGive != null)
            {
                QuestManager.Instance.AddQuest(currentDialogue.questToGive);
            }*/
        }
        currentNPC.onEndDialogue?.Invoke();
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

    public void UpdateDialogue(string npcName, int newDialogue)
    {
        foreach (string npc in m_npcDictionary.Keys.ToList())
        {
            if (npc == npcName)
            {
                Debug.Log($"Found {npcName}");
                m_npcDictionary[npc] = newDialogue;
                Debug.Log($"{npcName} is now at index {newDialogue}");
            }
        }
    }
}
