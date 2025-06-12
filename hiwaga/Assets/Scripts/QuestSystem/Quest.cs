using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Player;

public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public QuestGoal goal;
    public delegate void QuestFinishCallback();
    public static QuestFinishCallback onQuestComplete;

    public void FinishQuest()
    {
        Debug.Log($"\"{title}\" is finished.");
        QuestManager.Instance.RemoveQuest(this);
    }

    public void OnFinish()
    {
        if(onQuestComplete!=null)
        {
            onQuestComplete();
        }
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
