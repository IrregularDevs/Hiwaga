// using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
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

    public void AddItem(Item item, GameObject source)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                source.GetComponent<ItemSource>().ChangeUses(1);
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
                source.GetComponent<ItemSource>().ChangeUses(1);
                return;
            }
        }
        Debug.Log("Inventory is full!");
        return;
    }

    public void RemoveItem(Item item, GameObject receiver)
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
                receiver.GetComponent<ItemReceiver>().ChangeUses(1);
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
