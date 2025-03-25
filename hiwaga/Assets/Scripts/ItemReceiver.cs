using UnityEngine;

public class ItemReceiver : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private Item item;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private bool oneTime;
    private bool used = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
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
        if (used == true && oneTime == true)
        {
            return;
        }
        else
        {
            inventoryManager.RemoveItem(item);
            used = true;
        }
    }
}
