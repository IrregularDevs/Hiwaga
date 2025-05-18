using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public bool completed;
    public int requiredAmount;
    public int currentAmount;
}
