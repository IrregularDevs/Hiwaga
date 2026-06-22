#nullable enable
using UnityEngine;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    //Reference variables
    [SerializeField] private Item? currentItem = null;
    [SerializeField] private List<WorldItem> worldItems = new List<WorldItem>();

    //Change item in slot to another item
    public void ChangeItem(Item newItem)
    {
        if(worldItems.Exists(x => x.GetItem() == newItem))
        {
            worldItems.Find(x => x.GetItem() == currentItem)?.gameObject.SetActive(false);
            currentItem = newItem;
            worldItems.Find(x => x.GetItem() == currentItem)?.gameObject.SetActive(true);
        }
    }

    //Set item in slot to null
    public void RemoveItem()
    {
        worldItems.Find(x => x.GetItem() == currentItem)?.gameObject.SetActive(false);
        currentItem = null;
    }

    //Return current item
    public Item? GetCurrentItem()
    {
        return currentItem;
    }
}
