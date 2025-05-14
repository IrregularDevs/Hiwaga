using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void ItemFetched()
    {
        if(goalType == GoalType.Fetch)
        {
            currentAmount++;
        }
    }
}
