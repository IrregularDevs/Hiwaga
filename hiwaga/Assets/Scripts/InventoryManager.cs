// using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null || itemInSlot.item == null)
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
                itemInSlot.count--;
                if (itemInSlot.item.stackable == true)
                {
                    itemInSlot.RefreshCount();
                }
                if(itemInSlot.count <= 0)
                {
                    TakeItem(itemInSlot);
                }
                return;
            }
        }
        Debug.Log("You don't have what is required!");
        return;
    }


    public void SpawnItem(Item item, InventorySlot slot)
    {
        InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
        if (inventoryItem == null)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
            inventoryItem = newItem.GetComponent<InventoryItem>();
        }
        inventoryItem.InitializeItem(item);
        inventoryItem.RefreshCount();
    }

    public void TakeItem(InventoryItem itemInSlot)
    {
        itemInSlot.ClearItem();
    }
}
