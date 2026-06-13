using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcRefName;
    public int dialogueIndex;
    public DialogueLine[] dialogueLines;

    public bool[] autoProgressLines;

    public bool loops;

    public Quest questToGive;
}
