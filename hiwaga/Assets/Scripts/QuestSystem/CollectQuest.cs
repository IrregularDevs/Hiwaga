using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CollectQuest", menuName = "Scriptable Objects/CollectQuest")]
public class CollectQuest : Quest
{
    public Item requiredItem;

    public void ProgressUpdate()
    {
        if (goal.currentAmount >= goal.requiredAmount)
        {
            goal.completed = true;
            FinishQuest();
        }
    }

    public void AcceptQuest()
    {
        foreach (PlayerInventory playerInventory in Player.Instance.items)
        {
            if (playerInventory.item == requiredItem)
            {
                goal.currentAmount = playerInventory.count;
                ProgressUpdate();
            }
        }
    }

    public void ItemCheck(Item item, int count)
    {
        if (item == requiredItem)
        {
            goal.currentAmount = count;
            ProgressUpdate();
        }
    }

    public override void InitializeQuest()
    {
        Debug.Log("InitializeQuest override method called.");
        Player.onQuestAdd = AcceptQuest;
        Player.onInventoryUpdate += ItemCheck;
        if(Player.onQuestAdd == null)
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
        Player.onInventoryUpdate -= ItemCheck;
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
