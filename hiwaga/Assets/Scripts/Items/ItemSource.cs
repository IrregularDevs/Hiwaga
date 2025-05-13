using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ItemSource : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private Item item;
    [SerializeField] private bool hasLimit;
    [SerializeField] private int maxUses;
    private int uses = 0;

    public void enterPrompt()
    {
        Debug.Log(enterString);
    }

    public void exitPrompt()
    {
        Debug.Log(exitString);
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
            if(InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager is missing.");
            }
            InventoryManager.Instance.AddItem(item, this.gameObject);
            return;
        }
    }

    public void ChangeUses(int i)
    {
        uses += i;
    }
}