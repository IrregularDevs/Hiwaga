using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueGroup", menuName = "Scriptable Objects/DialogueGroup")]
public class DialogueGroup : ScriptableObject
{
    public List<DialogueData> dialogue = new List<DialogueData>();
    public string npcName;
    public int currentIndex;

    public void ChangeIndex(int i)
    {
        currentIndex = i;
    }
}
