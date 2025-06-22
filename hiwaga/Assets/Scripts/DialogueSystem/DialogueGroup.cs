using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueGroup", menuName = "Scriptable Objects/DialogueGroup")]
public class DialogueGroup : ScriptableObject
{
    public DialogueData[] dialogue;
}
