using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string dialogueLine;
    public NPCCharacter character;
    public float typingSpeed = 0.05f; // Time in seconds between each character being displayed
    public float autoProgressDelayFast = 1f; // Time in seconds before automatically progressing to the next line
    public float autoProgressDelaySlow = 2f;
}
