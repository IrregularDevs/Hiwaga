using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public void AddItem(Item item)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnItem(item, slot);
                return;
            }
        }
        Debug.Log("Inventory is full!");
        return;
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                TakeItem(itemInSlot);
                return;
            }
        }
        Debug.Log("You don't have what is required!");
        return;
    }


    public void SpawnItem(Item item, InventorySlot slot)
    {
        InventoryItem inventoryItem;
        if (slot.GetComponent<InventoryItem>() == null)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
            inventoryItem = newItem.GetComponent<InventoryItem>();
            inventoryItem.InitializeItem(item);
        }
        else
        {
            inventoryItem = slot.GetComponent<InventoryItem>();
            inventoryItem.InitializeItem(item);
        }
    }

    public void TakeItem(InventoryItem itemInSlot)
    {
        itemInSlot.ClearItem();
    }
}
