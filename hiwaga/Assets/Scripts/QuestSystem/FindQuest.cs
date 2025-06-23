using UnityEngine;

[CreateAssetMenu(fileName = "FindQuest", menuName = "Scriptable Objects/FindQuest")]
public class FindQuest : Quest
{
    public void ProgressUpdate()
    {
        goal.currentAmount++;
        if (goal.currentAmount >= goal.requiredAmount)
        {
            goal.completed = true;
            FinishQuest();
        }
    }

    public override void InitializeQuest()
    {
        goal.currentAmount = 0;
        Player.onQuestAdd = ProgressUpdate;
        onQuestComplete += ProgressDialogueQuestEnd;
        Debug.Log("FindQuest Initialized");
    }

    public override void EmptyQuest()
    {
        Player.onQuestAdd = null;
        onQuestComplete = null;

        if (!isLastQuest)
        {
            QuestManager.Instance.AddQuest(nextQuest);
        }

        Debug.Log("FindQuest Emptied");
    }
}
