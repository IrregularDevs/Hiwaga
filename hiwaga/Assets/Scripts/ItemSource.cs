using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ItemSource : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private Item item;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void enterPrompt()
    {
        Debug.Log(enterString);
        //Show an indicator to the player that they can interact with the object
    }

    public void exitPrompt()
    {
        Debug.Log(exitString);
        //remove said indicator from enterPrompt.
    }

    public void Interact()
    {
        Debug.Log(interactString);
    }
}