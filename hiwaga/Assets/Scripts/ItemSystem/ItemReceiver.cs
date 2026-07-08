using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemReceiver : ItemHolder
{
    /*public delegate void UseUpdateCallback();
    public UseUpdateCallback onUse;*/

    /*public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }
    public bool canInteract()
    {
        return true;
    }*/

    //Called when interacted with
    public override void Interact()
    {
        if (maxUses > 0 && currentUses >= maxUses)
        {
            onInvalidHolder?.Invoke();
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
            if (maxUses > 0 && currentUses >= maxUses)
            {
                onInvalidHolder?.Invoke();
            }
        }
        SetActivePrompt(true);
    }

    /*public int GetUses()
    {
        return uses;
    }*/
}
