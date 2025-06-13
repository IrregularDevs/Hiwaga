using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string[] npcName;
    public Sprite[] npcPortrait;
    public string[] dialogueLines;
    public bool[] dialogueSwitch;
    public float typingSpeed = 0.05f; // Time in seconds between each character being displayed
    public bool[] autoProgressLines;
    public float autoProgressDelay = 2f; // Time in seconds before automatically progressing to the next line
}
