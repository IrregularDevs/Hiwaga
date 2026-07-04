using UnityEngine;

public class HiddenFishObject : MonoBehaviour
{
    [Header("Set in Inspector")]
    public bool isCorrectFish = false;

    [Header("Fish Reference")]
    public GameObject fishModel;
    public FishServant_Benito fishServant; 

    private bool alreadyInteracted = false;

    public void Interact()
    {
        if (alreadyInteracted) return;

        alreadyInteracted = true;

        if (isCorrectFish)
        {
            RevealFish();
        }
        else
        {
            WrongChoice();
        }
    }

    void RevealFish()
    {
        Debug.Log("You found the fish!");

        // show model if you still use a separate visual object
        if (fishModel != null)
            fishModel.SetActive(true);

        // activate NPC fish
        if (fishServant != null)
            fishServant.RevealFish();
    }

    void WrongChoice()
    {
        Debug.Log("Nothing here...");
    }
}