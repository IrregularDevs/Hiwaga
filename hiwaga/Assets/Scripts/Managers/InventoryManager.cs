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

    [SerializeField] private GameObject panelInventory;

    [SerializeField] private int maxStackedItems;
    public static int maxStackedItemsStatic;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        maxStackedItemsStatic = maxStackedItems;
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }

    public void AddItem(ItemSource source)
    {
        ItemHeld[] itemsToAdd = source.GetItems();
        bool isItemAdded = false;

        if(!CanAddItem(itemsToAdd))
        {
            Debug.Log("Not enough space.");
            return;
        }
        foreach(ItemHeld itemHeld in itemsToAdd)
        {
            Item item = itemHeld.item;
            int amountGiven = itemHeld.count;
            isItemAdded = false;
            Player.UpdateInventory(item, amountGiven);
            while(amountGiven > 0)
            {
                Debug.Log("Adding item.");
                foreach (InventorySlot slot in inventorySlots)
                {
                    if (slot.item == item && slot.count < maxStackedItemsStatic && slot.item.stackable)
                    {
                        int oldCount = slot.count;
                        slot.UpdateCount(amountGiven);
                        amountGiven -= slot.count - oldCount;
                        if (slot.count > maxStackedItemsStatic)
                        {
                            slot.UpdateCount(maxStackedItemsStatic);
                        }
                        isItemAdded = true;
                        break;
                    }
                }
                if (!isItemAdded)
                {
                    foreach (InventorySlot slot in inventorySlots)
                    {
                        if (!slot.item)
                        {
                            slot.InitializeItem(item, amountGiven);
                            amountGiven -= slot.count;
                            isItemAdded = true;
                            break;
                        }
                    }
                }
            }
        }
        if (isItemAdded)
        {
            source.ChangeUses(1);
        }
        return;
    }

    public void RemoveItem(ItemReceiver receiver)
    {
        ItemHeld[] itemsToRemove = receiver.GetItems();
        bool isItemRemoved = false;

        if (!CanRemoveItem(itemsToRemove))
        {
            return;
        }

        foreach(ItemHeld itemHeld in itemsToRemove)
        {
            Item item = itemHeld.item;
            int amountTaken = -itemHeld.count;
            Player.UpdateInventory(item, amountTaken);
            while (amountTaken < 0)
            {
                foreach (InventorySlot slot in inventorySlots)
                {
                    if (slot.item == item)
                    {
                        int oldCount = -slot.count;
                        slot.UpdateCount(amountTaken);
                        amountTaken -=  oldCount - slot.count;
                        if (slot.count < 0)
                        {
                            slot.UpdateCount(-slot.count);
                            slot.ClearItem();
                        }
                        isItemRemoved = true;
                        break;
                    }
                }
            }
        }
        if(isItemRemoved)
        {
            receiver.onUse?.Invoke();
            receiver.ChangeUses(1);
        }
        return;
    }

    public int ClampValue(int count)
    {
        return Mathf.Clamp(count, 0, maxStackedItems);
    }

    public bool CanAddItem(ItemHeld[] itemsToAdd)
    {
        Debug.Log("Checking if item can be added.");
        bool[] takenSlots = new bool[inventorySlots.Length];

        foreach(ItemHeld itemHeld in itemsToAdd)
        {
            int spaceAvailable = 0;
            int i = 0;
            foreach (InventorySlot slot in inventorySlots)
            {
                if (!takenSlots[i] && (slot.item == itemHeld.item || slot.item == null))
                {
                    spaceAvailable += itemHeld.item.stackable ? maxStackedItemsStatic - slot.count : 1;
                    takenSlots[i] = true;
                }
                i++;
            }
            if(spaceAvailable < itemHeld.count)
            {
                Debug.Log("Not enough space.");
                return false;
            }
        }
        return true;
    }

    public bool CanRemoveItem(ItemHeld[] itemsToRemove)
    {
        foreach(ItemHeld itemHeld in itemsToRemove)
        {
            if(!Player.items.Exists(x => x.item == itemHeld.item) || Player.items.Find(x => x.item == itemHeld.item).count < itemHeld.count)
            {
                return false;
            }
        }
        return true;
    }

    public void ShowHidePanel(bool state)
    {
        panelInventory.SetActive(state);
    }

}
