using UnityEngine;

public class ItemReceiver : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private Item item;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private bool hasLimit;
    [SerializeField] private int maxUses;
    private int uses = 0;
    private AudioSource audioSource;

    private void Start()
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
        if (hasLimit == true && uses >= maxUses)
        {
            return;
        }
        else
        {
            if (inventoryManager == null)
            {
                inventoryManager = FindAnyObjectByType<InventoryManager>();
            }
            inventoryManager.RemoveItem(item, this.gameObject);
            return;
        }
    }

    public void ChangeUses(int i)
    {
        uses += i;
    }
}
