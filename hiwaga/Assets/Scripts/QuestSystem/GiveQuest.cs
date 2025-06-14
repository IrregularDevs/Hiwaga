using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "GiveQuest", menuName = "Scriptable Objects/GiveQuest")]
public class GiveQuest : Quest
{
    public override void InitializeQuest()
    {
        goal.completed = false;
        goal.currentAmount = 0;
        onQuestComplete += ProgressDialogue;
        Debug.Log("InitializeQuest override method called.");
        if (Player.onQuestAdd == null)
        {
            Debug.Log("onQuestAdd is empty.");
        }
        if (Player.onInventoryUpdate == null)
        {
            Debug.Log("onInventoryUpdate is empty.");
        }
    }

    public override void EmptyQuest()
    {
        Debug.Log("EmptyQuest override method called.");
        if (!isLastQuest)
        {
            QuestManager.Instance.AddQuest(nextQuest);
        }
        if (Player.onQuestAdd == null)
        {
            Debug.Log("onQuestAdd is empty.");
        }
        if (Player.onInventoryUpdate == null)
        {
            Debug.Log("onInventoryUpdate is empty.");
        }
    }
}
