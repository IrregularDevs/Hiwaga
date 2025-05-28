using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ItemSource : ItemHolder, IInteractable
{
    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }

    public void Interact()
    {
        if (hasLimit == true && uses >= maxUses)
        {
            return;
        }
        else
        {
            if(InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager is missing.");
                return;
            }
            foreach(KeyValuePair<Item, int> item in items)
            {
                InventoryManager.Instance.AddItem(item.Key, this, item.Value);
            }
            ChangeUses(1);
            return;
        }
    }
}