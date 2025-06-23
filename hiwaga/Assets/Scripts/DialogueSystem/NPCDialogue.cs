using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public DialogueLine[] dialogueLines;

    public float typingSpeed = 0.05f; // Time in seconds between each character being displayed
    public bool[] autoProgressLines;
    public float autoProgressDelayFast = 1f; // Time in seconds before automatically progressing to the next line
    public float autoProgressDelaySlow = 2f;

    public bool loops;

    public Quest questToGive;
}
