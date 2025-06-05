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

    private void Update()
    {
        if (Input.GetButtonDown("Confirm") && InteractionManager.Instance.interactTarget && InteractionManager.Instance.IsInRange)
        {
            InteractionManager.Instance.interactTarget.GetComponent<IInteractable>().Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().enterPrompt();
            InteractionManager.Instance.interactTarget = other.gameObject;
            InteractionManager.Instance.IsInRange = true;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
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
            other.gameObject.GetComponent<IInteractable>().exitPrompt();
            InteractionManager.Instance.interactTarget = null;
            InteractionManager.Instance.IsInRange = false;
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
