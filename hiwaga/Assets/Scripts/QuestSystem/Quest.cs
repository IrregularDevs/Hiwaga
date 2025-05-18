/*using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;

    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        Debug.Log("Quest Complete.");
    }

}*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CollectQuest;
using static Player;

public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public QuestGoal goal;
    public InventoryUpdateCallback onFetch;

    public void FinishQuest()
    {
        Debug.Log($"{title} is finished.");
    }
}
