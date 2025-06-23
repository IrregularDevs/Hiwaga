using UnityEngine;
using System.Collections;

public class FindQuestObjective : MonoBehaviour, IInteractable
{
    private bool playerInRange = false;
    private bool isHiding = false;

    [SerializeField] private Transform[] hideSpots;
    [SerializeField] private float hideDelay = 1.0f; // Delay before hiding again
    [SerializeField] private FindQuest findQuest;

    private void Start()
    {
        findQuest.onQuestAdd = HideInRandomSpot;
    }

    public void Interact()
    {
        Debug.Log("You found Hopkins!");

        if (Player.Instance.quests.Contains(findQuest))
        {
            Debug.Log("Progressing quest.");

            findQuest.ProgressUpdate(); // Progress the quest
            StartCoroutine(HideAgain());
        }
    }

    public void enterPrompt()
    {
        playerInRange = true;
        Debug.Log("You feel like someone's watching...");
    }

    public void exitPrompt()
    {
        playerInRange = false;
    }

    public bool canInteract()
    {
        return playerInRange && !isHiding;
    }

    public void HideInRandomSpot()
    {
        if (hideSpots.Length == 0)
        {
            Debug.LogWarning("No hide spots assigned!");
            return;
        }

        int index = Random.Range(0, hideSpots.Length);
        transform.position = hideSpots[index].position;
        gameObject.SetActive(true);
        isHiding = false;
    }

    private IEnumerator HideAgain()
    {
        isHiding = true;

        // Optional: play a particle effect or disappear animation here
        Debug.Log("Hopkins is hiding again...");

        // Check if the quest is still incomplete before hiding again

        if (Player.Instance.quests.Contains(findQuest))
        {
            HideInRandomSpot(); // Hide again in a new place
        }
        else
        {
            Debug.Log("Hopkins has been found enough times. Quest complete.");
        }

        yield return null;

    }
}
