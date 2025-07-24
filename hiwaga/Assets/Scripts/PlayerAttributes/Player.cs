using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class PlayerInventory
{
    public Item item;
    public int count;
}

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance;

    public static List<Quest> quests = new List<Quest>();
    public static List<PlayerInventory> items = new List<PlayerInventory>();

    public delegate void InventoryUpdateCallback(Item item, int count);
    public static InventoryUpdateCallback onInventoryUpdate;

    public delegate void QuestUpdateCallback();
    public static QuestUpdateCallback onQuestAdd;

    public delegate void CollisionUpdateCallback();
    public static CollisionUpdateCallback onCollision;

    public delegate void InteractCallback();
    public static InteractCallback onInteract;

    public string playerName;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        playerName = "Muad'Dib";
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Confirm") && InteractionManager.interactTarget != null && InteractionManager.IsInRange)
        {
            onInteract?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onCollision?.Invoke();
        onInteract = null;
        IInteractable[] interactable = other.gameObject.GetComponents<IInteractable>();
        if (interactable != null)
        {
            InteractionManager.interactTarget = interactable;
            InteractionManager.IsInRange = true;
            foreach(IInteractable obj in InteractionManager.interactTarget)
            {
                obj.enterPrompt();
                onInteract += obj.Interact;
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        onCollision?.Invoke();
        IInteractable[] interactable = other.gameObject.GetComponents<IInteractable>();
        if (interactable != null)
        {
            if (InteractionManager.interactTarget == null)
            {
                InteractionManager.interactTarget = interactable;
            }
            if(onInteract == null)
            {
                foreach (IInteractable obj in InteractionManager.interactTarget)
                {
                    obj.enterPrompt();
                    onInteract += obj.Interact;
                }
            }
            InteractionManager.IsInRange = true;
        }
        else
        {
            InteractionManager.IsInRange = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable[] interactable = other.gameObject.GetComponents<IInteractable>();
        if (interactable != null)
        {
            InteractionManager.interactTarget = null;
            InteractionManager.IsInRange = false;
            foreach (IInteractable obj in interactable)
            {
                obj.exitPrompt();
                onInteract -= obj.Interact;
            }
        }
        else
        {
            return;
        }
    }

    public static void UpdateInventory(Item newItem, int amount)
    {
        if(items.Contains(items.Find(x=>x.item==newItem)))
        {
            items.Find(x=>x.item == newItem).count += amount;
        }
        else
        {
            items.Add(new PlayerInventory() { item = newItem, count = amount });
        }
        onInventoryUpdate?.Invoke(newItem, items.Find(x => x.item == newItem).count);
    }
}
