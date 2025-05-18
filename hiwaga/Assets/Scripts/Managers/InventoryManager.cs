// using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
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

    public void AddItem(Item item, ItemSource source)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                ClampValue(itemInSlot.count += source.amountGiven);
                itemInSlot.RefreshCount();
                Player.onInventoryUpdate(item, itemInSlot.count);
                source.ChangeUses(1);
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
                itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if(itemInSlot == null)
                {
                    Debug.Log("What the fuck");
                }
                ClampValue(itemInSlot.count += source.amountGiven - 1);
                itemInSlot.RefreshCount();
                Player.onInventoryUpdate(item, itemInSlot.count);
                source.ChangeUses(1);
                return;
            }
        }
        Debug.Log("Inventory is full!");
        return;
    }

    public void RemoveItem(Item item, ItemReceiver receiver)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item)
            {
                ClampValue(itemInSlot.count -= receiver.amountTaken);
                Player.onInventoryUpdate(item, itemInSlot.count);
                if (itemInSlot.item.stackable == true)
                {
                    itemInSlot.RefreshCount();
                }
                if (itemInSlot.count <= 0)
                {
                    TakeItem(itemInSlot);
                }
                receiver.ChangeUses(1);
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

    public void ClampValue(int count)
    {
        Mathf.Clamp(count, 0, maxStackedItems);
    }
}
