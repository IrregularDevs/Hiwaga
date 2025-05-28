using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToAdd
{
    public Item itemAdded;
    public int amountAdded;
}

public class ItemHolder : MonoBehaviour
{
    [SerializeField] protected string enterString, exitString, interactString;
    [SerializeField] protected Dictionary<Item, int> items = new Dictionary<Item, int>();
    [SerializeField] protected List<ItemToAdd> itemToAdd = new List<ItemToAdd>();
    [SerializeField] protected bool hasLimit;
    [SerializeField] protected int maxUses;
    [SerializeField] protected int uses = 0;
    
    public void ChangeUses(int i)
    {
        uses += i;
    }

    protected void Start()
    {
        foreach(ItemToAdd item in itemToAdd)
        {
            items.Add(item.itemAdded, item.amountAdded);
        }
    }
}
