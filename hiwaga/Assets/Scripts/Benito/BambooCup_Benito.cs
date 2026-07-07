using UnityEngine;

public class BambooCup_Benito : ItemReceiver
{
    [SerializeField] private Bird_Benito bird;

    private void Start()
    {
        DialogueManager.Instance.UpdateDialogue("Sparrowhawk", 3);
        onInvalidHolder -= CheckGoal;
        onInvalidHolder += CheckGoal;
    }

    private void OnDisable()
    {
        onInvalidHolder -= CheckGoal;
    }

    private void CheckGoal()
    {
        DialogueManager.Instance.UpdateDialogue("Sparrowhawk", 4);
        bird.ProgressStory(true);
    }
}
