using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public string description { get; set; }
    public bool completed { get; set; }
    public int requiredAmount { get; set; }
    public int currentAmount { get; set; }

    public virtual void Init()
    {

    }

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

    public void Evaluate()
    {
        if(currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        completed = true;
    }
}
