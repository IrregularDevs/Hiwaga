using UnityEngine;

public class GiveQuestObjective : MonoBehaviour
{
    public GiveQuest quest;
    public ItemReceiver receiver;
    public int goal;

    public void Update()
    {
        if(receiver.GetUses() >= goal)
        {
            quest.goal.completed = true;
            quest.goal.currentAmount = 1;
            quest.FinishQuest();
            quest.EmptyQuest();
        }
    }
}
