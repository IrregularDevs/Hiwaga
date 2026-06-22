using UnityEngine;

public class Area6To8Awake_Benito : ItemSource
{
    private void Start()
    {
        if(!Player.Instance.HasItem(itemHeld))
        {
            InventoryManager.Instance.AddItem(this);
        }
    }
}
