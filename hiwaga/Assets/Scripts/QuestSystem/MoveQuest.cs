using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MoveQuest", menuName = "Scriptable Objects/MoveQuest")]
public class MoveQuest : Quest
{
    public bool isTouched;

    public void ProgressUpdate()
    {
        if (goal.currentAmount >= goal.requiredAmount && isTouched)
        {
            goal.completed = true;
            FinishQuest();
        }
    }

    public override void InitializeQuest()
    {
        isTouched = false;
        goal.currentAmount = 0;
        Player.onQuestAdd = ProgressUpdate;
        Player.onCollision += ProgressUpdate;
        onQuestComplete += ProgressDialogueQuestEnd;
        if (Player.onQuestAdd == null)
        {
            Debug.Log("onQuestAdd is empty.");
        }
    }

    public override void EmptyQuest()
    {
        Player.onCollision -= ProgressUpdate;
        onQuestComplete = null;
        if (!isLastQuest)
        {
            QuestManager.Instance.AddQuest(nextQuest);
        }
        if (Player.onQuestAdd == null)
        {
            Debug.Log("onQuestAdd is empty.");
        }
    }
}
