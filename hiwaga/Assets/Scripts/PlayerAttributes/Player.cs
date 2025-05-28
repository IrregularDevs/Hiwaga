using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance;

    public List<Quest> quests = new List<Quest>();
    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    public delegate void InventoryUpdateCallback(Item item, int count);
    public static InventoryUpdateCallback onInventoryUpdate;

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

    public void UpdateInventory(Item item, int count)
    {
        if(count <= 0 && items.ContainsKey(item))
        {
            items.Remove(item);
        }
        if (items.ContainsKey(item))
        {
            items[item] = count;
        }
        else
        {
            items.Add(item, count);
        }
        if(onInventoryUpdate!=null)
        {
            onInventoryUpdate(item, items[item]);
        }
    }
}
