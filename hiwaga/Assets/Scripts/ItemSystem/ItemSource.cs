using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ItemSource : ItemHolder
{
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
            InventoryManager.Instance.AddItem(this);
            if (maxUses > 0 && currentUses >= maxUses)
            {
                onInvalidHolder?.Invoke();
            }
            //return;
        }
    }

    /*public int GetUses()
    {
        return uses;
    }*/
}