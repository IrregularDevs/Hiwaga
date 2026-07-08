using UnityEngine;

public class BambooCup_Benito : ItemReceiver
{
    [SerializeField] private Bird_Benito bird;
    [SerializeField] private int initialDialogue, newDialogue;

    private void Start()
    {
        DialogueManager.Instance.UpdateDialogue("Sparrowhawk", initialDialogue);
        onInvalidHolder -= CheckGoal;
        onInvalidHolder += CheckGoal;
    }

    private void OnDisable()
    {
        onInvalidHolder -= CheckGoal;
    }

    private void CheckGoal()
    {
        DialogueManager.Instance.UpdateDialogue("Sparrowhawk", newDialogue);
        bird.ProgressStory(true);
    }
}
