using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ItemSource : ItemHolder
{
    public override void Interact()
    {
        DisappearCheck();
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager is missing.");
            return;
        }
        InventoryManager.Instance.AddItem(this);
        DisappearCheck();
    }
}