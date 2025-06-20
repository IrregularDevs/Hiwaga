using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class NPCCharacterNumbered
{
    public NPCCharacter character;
    public int id;
}

[System.Serializable]
public class DialogueLine
{
    public string dialogueLine;
    public int dialogueSwitch;
}

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public NPCCharacterNumbered[] characters;
    public DialogueLine[] dialogueLines;
    public float typingSpeed = 0.05f; // Time in seconds between each character being displayed
    public bool[] autoProgressLines;
    public float autoProgressDelay = 2f; // Time in seconds before automatically progressing to the next line
}
