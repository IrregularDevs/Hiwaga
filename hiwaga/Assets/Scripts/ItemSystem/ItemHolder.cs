using System.Collections.Generic;
using UnityEngine;

/*[System.Serializable]
public class ItemHeld
{
    public Item item;
    public int count;
}*/

public class ItemHolder : Interactable
{
    /*[SerializeField] protected string enterString, exitString, interactString;
    [SerializeField] protected List<ItemHeld> itemsHeld = new List<ItemHeld>();
    [SerializeField] protected bool hasLimit;
    [SerializeField] protected bool disappears;*/

    //Item given
    [SerializeField] protected Item itemHeld;

    //Uses. Negative maxUses for infinite source
    [SerializeField] protected int maxUses;
    [SerializeField] protected int currentUses = 0;

    //When holder is no longer in use
    public delegate void OnInvalidHolder();
    public static OnInvalidHolder onInvalidHolder;

    //Count uses
    public void AddCurrentUses(int i)
    {
        currentUses += i;
    }

    //Return item
    public Item GetItem()
    {
        return itemHeld;
    }

    //Return current uses
    public int GetUses()
    {
        return currentUses;
    }

}
