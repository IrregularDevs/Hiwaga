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
    public OnInvalidHolder onInvalidHolder;

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

    public override void SetActivePrompt(bool state)
    {
        base.SetActivePrompt(state);
        if (currentUses >= maxUses && maxUses > 0)
        {
            base.SetActivePrompt(false);
        }
    }
}
