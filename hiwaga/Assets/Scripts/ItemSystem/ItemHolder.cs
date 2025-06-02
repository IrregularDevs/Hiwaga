using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemHeld
{
    public Item item;
    public int count;
}

public class ItemHolder : MonoBehaviour
{
    [SerializeField] protected string enterString, exitString, interactString;
    [SerializeField] protected List<ItemHeld> itemsHeld = new List<ItemHeld>();
    [SerializeField] protected bool hasLimit;
    [SerializeField] protected int maxUses;
    [SerializeField] protected int uses = 0;
    
    public void ChangeUses(int i)
    {
        uses += i;
    }

    public List<ItemHeld> GetItems()
    {
        return itemsHeld;
    }
}
