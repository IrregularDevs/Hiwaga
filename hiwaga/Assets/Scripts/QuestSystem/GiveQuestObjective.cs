using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiveQuestObjective : MonoBehaviour
{
    public GiveQuest quest;
    public ItemReceiver receiver;
    public int goal;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        receiver.onUse += ProgressQuest;
        yield return null;
    }

    public void Update()
    {
        if(receiver.GetUses() >= goal)
        {
            quest.goal.completed = true;
            quest.goal.currentAmount = 1;
            quest.FinishQuest();
        }
    }

    public void ProgressQuest()
    {
        quest.FinishQuest();
    }
}
