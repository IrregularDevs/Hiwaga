using UnityEngine;

public class MoveQuestObjective : MonoBehaviour
{
    public MoveQuest quest;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            quest.isTouched = true;
            quest.goal.currentAmount = 1;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            quest.isTouched = true;
            quest.goal.currentAmount = 1;
        }
    }
}
