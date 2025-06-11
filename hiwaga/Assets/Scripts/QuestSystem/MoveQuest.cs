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
        Debug.Log("InitializeQuest override method called.");
        Player.onQuestAdd = ProgressUpdate;
        Player.onCollision += ProgressUpdate;
        if (Player.onQuestAdd == null)
        {
            Debug.Log("onQuestAdd is empty.");
        }
    }

    public override void EmptyQuest()
    {
        Debug.Log("EmptyQuest override method called.");
        Player.onCollision -= ProgressUpdate;
        if (Player.onQuestAdd == null)
        {
            Debug.Log("onQuestAdd is empty.");
        }
    }
}
