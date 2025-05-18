using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CollectQuest", menuName = "Scriptable Objects/CollectQuest")]
public class CollectQuest : Quest
{
    public Item requiredItem;

    private void Awake()
    {
        onFetch += ItemCheck;
        onFetch += ProgressUpdate;
    }

    public void ItemCheck(Item item, int count)
    {
        if (item == requiredItem)
        {
            goal.currentAmount = count;
        }
    }

    public void ProgressUpdate(Item item, int count)
    {
        if(goal.currentAmount >= goal.requiredAmount)
        {
            goal.completed = true;
            FinishQuest();
        }
    }
}
