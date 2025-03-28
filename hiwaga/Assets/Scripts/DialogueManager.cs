using System.Collections.Generic;
// using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueManager", menuName = "Scriptable Objects/DialogueManager")]
public class DialogueManager : ScriptableObject
{
    [Tooltip("The portrait of the character")]
    public Sprite portrait;
    [Tooltip("The name of the character")]  
    public string character_name;
    [Tooltip("The dialogues of the character, Press the Plus to add to the list if you have Multiple lines")]  
    public List<string> dialogues;
    [Tooltip("Check if the dialogue requires an item to be shown")]
    public bool requiredItem;
    [Tooltip("The dialogue if the character doesn't have the required item")]
    public string noRequiredItemDialogue;
    [Tooltip("The dialogue if the character has the required item")]
    public string requiredItemDialogue;
    [Tooltip("The item required for the dialogue")]
    public Item questItem;
}

