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
using static Player;

public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public QuestGoal goal;

    public void FinishQuest()
    {
        Debug.Log($"\"{title}\" is finished.");
        QuestManager.Instance.RemoveQuest(this);
    }

    public void OnFinish()
    {
        EmptyQuest();
    }

    public virtual void InitializeQuest()
    {
        Debug.Log("InitializeQuest virtual method called.");
    }

    public virtual void EmptyQuest()
    {
        Debug.Log("EmptyQuest virtual method called.");
    }
}
