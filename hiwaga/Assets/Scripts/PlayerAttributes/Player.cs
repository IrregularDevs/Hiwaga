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

    public List<Quest> quests = new List<Quest>();
    public List<PlayerInventory> items = new List<PlayerInventory>();

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
        if (Input.GetButtonDown("Confirm") && InteractionManager.Instance.interactTarget && InteractionManager.Instance.IsInRange)
        {
            //InteractionManager.Instance.interactTarget.GetComponent<IInteractable>().Interact();
            if(onInteract != null)
            {
                onInteract();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(onCollision!=null)
        {
            onCollision();
        }
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            int i = 0;
            other.gameObject.GetComponent<IInteractable>().enterPrompt();
            InteractionManager.Instance.interactTarget = other.gameObject;
            InteractionManager.Instance.IsInRange = true;
            foreach(IInteractable interactable in other.gameObject.GetComponents<IInteractable>())
            {
                i++;
                onInteract += interactable.Interact;
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (onCollision != null)
        {
            onCollision();
        }
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            if (InteractionManager.Instance.interactTarget == null)
            {
                InteractionManager.Instance.interactTarget = other.gameObject;
            }
            InteractionManager.Instance.IsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            int i = 0;
            other.gameObject.GetComponent<IInteractable>().exitPrompt();
            InteractionManager.Instance.interactTarget = null;
            InteractionManager.Instance.IsInRange = false;
            foreach (IInteractable interactable in other.gameObject.GetComponents<IInteractable>())
            {
                i++;
                onInteract -= interactable.Interact;
            }
        }
        else
        {
            return;
        }
    }

    public void UpdateInventory(Item newItem, int amount)
    {
        if(items.Contains(items.Find(x=>x.item==newItem)))
        {
            items.Find(x=>x.item == newItem).count += amount;
        }
        else
        {
            items.Add(new PlayerInventory() { item = newItem, count = amount });
        }
        if(onInventoryUpdate != null)
        {
            onInventoryUpdate(newItem, items.Find(x => x.item == newItem).count);
        }
        else
        {
            Debug.Log("onInventoryUpdate is still empty.");
        }
    }
}
