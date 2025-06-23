using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HopkinsNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Quest questToGive;
    [SerializeField] private FindQuestObjective objectiveToHide;
    private bool playerInRange = false;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        questToGive.onQuestAccept += InitiateHopkinsQuest;
        yield return null;
    }

    public void Interact()
    {
        if (!canInteract()) return;
    }

    public void InitiateHopkinsQuest()
    {
        gameObject.SetActive(false);
        objectiveToHide.gameObject.SetActive(true);
    }

    public void enterPrompt() => playerInRange = true;
    public void exitPrompt() => playerInRange = false;
    public bool canInteract() => playerInRange;
}
