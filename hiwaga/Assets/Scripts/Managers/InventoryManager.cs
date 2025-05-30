// using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using static Unity.VisualScripting.Member;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    public int maxStackedItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }

    public void AddItem(ItemSource source)
    {
        List<ItemHeld> itemsToAdd = new List<ItemHeld>();
        itemsToAdd = source.GetItemsToAdd();
        bool isItemAdded = false;

        if(AvailableInventorySpace() < itemsToAdd.Count)
        {
            Debug.Log("Inventory is full.");
            return;
        }

        foreach(ItemHeld itemHeld in itemsToAdd)
        {
            Debug.Log($"Now adding {itemHeld.count} {itemHeld.item.name}.");
            Item item = itemHeld.item;
            int amountGiven = itemHeld.count;
            isItemAdded = false;
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                Debug.Log($"Checking slot {i + 1}");
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
                {
                    Debug.Log("Added item exists in current Inventory.");
                    itemInSlot.count += amountGiven;
                    itemInSlot.RefreshCount();
                    Player.Instance.UpdateInventory(item, amountGiven);
                    if (itemInSlot.count > maxStackedItems)
                    {
                        int leftover;
                        leftover = itemInSlot.count - maxStackedItems;
                        itemInSlot.count = maxStackedItems;
                        itemInSlot.RefreshCount();
                        for (int o = 0; o < inventorySlots.Length; o++)
                        {
                            slot = inventorySlots[o];
                            itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                            if (itemInSlot == null || itemInSlot.item == null)
                            {
                                SpawnItem(item, slot);
                                itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                                itemInSlot.count += leftover;
                                itemInSlot.RefreshCount();
                                break;
                            }
                        }
                    }
                    isItemAdded = true;
                    break;
                }
            }
            if(isItemAdded == false)
            {
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                    if (itemInSlot == null || itemInSlot.item == null)
                    {
                        Debug.Log($"Slot found at {i + 1}. Adding item there.");
                        SpawnItem(item, slot);
                        itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                        if (itemInSlot == null)
                        {
                            Debug.Log("What the fuck");
                        }
                        itemInSlot.count += amountGiven;
                        itemInSlot.RefreshCount();
                        Player.Instance.UpdateInventory(item, amountGiven);
                        isItemAdded = true;
                        break;
                    }
                }
            }
        }
        if(isItemAdded)
        {
            source.ChangeUses(1);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
        return;
    }

    public void RemoveItem(ItemReceiver receiver)
    {
        List<ItemHeld> itemsToAdd = new List<ItemHeld>();
        itemsToAdd = receiver.GetItemsToAdd();
        bool isItemRemoved = false;
        foreach(ItemHeld itemHeld in itemsToAdd)
        {
            Item item = itemHeld.item;
            int amountTaken = itemHeld.count;
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot.count > amountTaken)
                {
                    break;
                }
                if (itemInSlot != null && itemInSlot.item == item)
                {
                    itemInSlot.count -= amountTaken;
                    itemInSlot.count = ClampValue(itemInSlot.count);
                    Player.Instance.UpdateInventory(item, amountTaken);
                    itemInSlot.RefreshCount();
                    if (itemInSlot.count <= 0)
                    {
                        itemInSlot.ClearItem();
                    }
                    isItemRemoved = true;
                    break;
                }
            }
        }
        if(isItemRemoved)
        {
            receiver.ChangeUses(1);
        }
        else
        {
            Debug.Log("You don't have what is required!");
        }
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

    public int ClampValue(int count)
    {
        return Mathf.Clamp(count, 0, maxStackedItems);
    }

    public int AvailableInventorySpace()
    {
        int m = 0;
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null || itemInSlot.item == null)
            {
                m++;
            }
        }
        return m;
    }
}
