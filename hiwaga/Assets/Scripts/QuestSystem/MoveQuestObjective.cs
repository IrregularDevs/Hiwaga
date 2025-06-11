using UnityEngine;

public class MoveQuestObjective : MonoBehaviour
{
    public MoveQuest quest;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something touched.");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched.");
            quest.isTouched = true;
            quest.goal.currentAmount = 1;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Something is being touched.");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is being touched.");
            quest.isTouched = true;
            quest.goal.currentAmount = 1;
        }
    }
}
