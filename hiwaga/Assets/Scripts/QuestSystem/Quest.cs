using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Player;

public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public QuestGoal goal;

    public Quest nextQuest;
    public bool isLastQuest;

    public delegate void QuestFinishCallback();
    public QuestFinishCallback onQuestComplete;

    public bool progressesDialogue;
    public int questStartIndex;
    public int questEndIndex;
    public DialogueGroup dialogueGroup;

    public QuestUpdateCallback onQuestAdd;

    public void FinishQuest()
    {
        Debug.Log($"\"{title}\" is finished.");
        OnFinish();
        QuestManager.Instance.RemoveQuest(this);
        EmptyQuest();
    }

    public void OnFinish()
    {
        if(onQuestComplete!=null)
        {
            onQuestComplete();
        }
    }

    public virtual void InitializeQuest()
    {
        Debug.Log("InitializeQuest virtual method called.");
    }

    public virtual void EmptyQuest()
    {
        Debug.Log("EmptyQuest virtual method called.");
    }

    public void ProgressDialogueQuestStart()
    {
        if(progressesDialogue)
        {
            DialogueManager.Instance.ChangeIndex(dialogueGroup, questStartIndex);
        }
    }

    public void ProgressDialogueQuestEnd()
    {
        Debug.Log("Progressing Dialogue.");
        if (progressesDialogue)
        {
            DialogueManager.Instance.ChangeIndex(dialogueGroup, questEndIndex);
        }
    }
}
