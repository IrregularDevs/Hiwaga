using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class RequiredItem
{
    public Item item;
    public int currentCount;
    public int goalCount;
    public bool isFinished;
}

[CreateAssetMenu(fileName = "CollectQuest", menuName = "Scriptable Objects/CollectQuest")]
public class CollectQuest : Quest
{
    public List<RequiredItem> requiredItem = new List<RequiredItem>();

    public void ProgressUpdate()
    {
        foreach(RequiredItem reqItem in requiredItem)
        {
            if (reqItem.currentCount >= reqItem.goalCount && !reqItem.isFinished)
            {
                goal.currentAmount++;
                reqItem.isFinished = true;
            }
            if (goal.currentAmount >= goal.requiredAmount)
            {
                goal.completed = true;
                FinishQuest();
            }
        }
    }

    public void AcceptQuest()
    {
        foreach(RequiredItem reqItem in requiredItem)
        {
            foreach (PlayerInventory playerInventory in Player.Instance.items)
            {
                if (playerInventory.item == reqItem.item)
                {
                    reqItem.currentCount = playerInventory.count;
                    ProgressUpdate();
                }
            }
        }
    }

    public void ItemCheck(Item item, int count)
    {
        foreach (RequiredItem reqItem in requiredItem)
        {
            if (item == reqItem.item)
            {
                reqItem.currentCount = count;
                ProgressUpdate();
            }
        }
    }

    public override void InitializeQuest()
    {
        goal.requiredAmount = requiredItem.Count;
        goal.currentAmount = 0;
        foreach (RequiredItem item in requiredItem)
        {
            item.currentCount = 0;
            item.isFinished = false;
        }
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
        onQuestComplete = null;
        if(!isLastQuest)
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
