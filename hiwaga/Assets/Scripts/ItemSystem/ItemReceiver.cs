using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemReceiver : ItemHolder, IInteractable
{
    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }
    public bool canInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (hasLimit == true && uses >= maxUses)
        {
            return;
        }
        else
        {
            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager is missing.");
                return;
            }
            InventoryManager.Instance.RemoveItem(this);
            return;
        }
    }
}
