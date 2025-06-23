using UnityEngine;
using System.Collections;

public class FindObjective : MonoBehaviour, IInteractable
{
    private bool playerInRange = false;
    private bool isHiding = false;

    [SerializeField] private Transform[] hideSpots;
    [SerializeField] private float hideDelay = 1.0f; // Delay before hiding again

    private void Start()
    {
        HideInRandomSpot();
    }

    public void Interact()
    {
        if (!canInteract()) return;

        Debug.Log("You found Hopkins!");

        Player.onQuestAdd?.Invoke(); // Progress the quest

        StartCoroutine(HideAgain());
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

        gameObject.SetActive(false);

        yield return new WaitForSeconds(hideDelay);

        // Check if the quest is still incomplete before hiding again
        Quest currentQuest = Player.Instance.quests.Find(q => q.title == "Find Me hehe....");

        if (currentQuest != null && !currentQuest.goal.completed)
        {
            HideInRandomSpot(); // Hide again in a new place
        }
        else
        {
            Debug.Log("Hopkins has been found enough times. Quest complete.");
        }
    }
}
