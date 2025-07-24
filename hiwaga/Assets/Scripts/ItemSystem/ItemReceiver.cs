using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemReceiver : ItemHolder
{
    public delegate void UseUpdateCallback();
    public UseUpdateCallback onUse;

    public override void Interact()
    {
        DisappearCheck();
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager is missing.");
            return;
        }
        InventoryManager.Instance.RemoveItem(this);
        DisappearCheck();
    }
}
