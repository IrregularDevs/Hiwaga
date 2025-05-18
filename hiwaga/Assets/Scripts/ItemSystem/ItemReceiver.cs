using UnityEngine;

public class ItemReceiver : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private Item item;
    [SerializeField] private bool hasLimit;
    [SerializeField] private int maxUses;
    public int amountTaken;
    private int uses = 0;

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
            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager not found.");
            }
            InventoryManager.Instance.RemoveItem(item, this);
            return;
        }
    }

    public void ChangeUses(int i)
    {
        uses += i;
    }
}
