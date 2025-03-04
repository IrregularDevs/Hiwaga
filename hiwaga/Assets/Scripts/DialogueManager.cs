using UnityEngine;

[CreateAssetMenu(fileName = "DialogueManager", menuName = "Scriptable Objects/DialogueManager")]
public class DialogueManager : ScriptableObject
{
    public Sprite portrait;
    public string character_name;
    public string dialogue;
}

