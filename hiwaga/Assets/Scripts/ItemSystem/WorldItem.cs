using UnityEngine;

public class WorldItem : MonoBehaviour
{
    //Reference variables
    [SerializeField] private Item item;
    [SerializeField] private GameObject model;

    //Return item
    public Item GetItem()
    {
        return item;
    }
}
