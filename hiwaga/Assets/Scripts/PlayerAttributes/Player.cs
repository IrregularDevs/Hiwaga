using UnityEngine;
using UnityEngine.InputSystem;
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

    /*public List<Quest> quests = new List<Quest>();
    public List<PlayerInventory> items = new List<PlayerInventory>();

    public delegate void InventoryUpdateCallback(Item item, int count);
    public static InventoryUpdateCallback onInventoryUpdate;

    public delegate void QuestUpdateCallback();
    public static QuestUpdateCallback onQuestAdd;

    public delegate void CollisionUpdateCallback();
    public static CollisionUpdateCallback onCollision;

    public delegate void InteractCallback();
    public static InteractCallback onInteract;*/

    public string playerName;
    public PlayerInputAction controls;

    private void Awake()
    {
        instance = this;
        playerName = "Muad'Dib";
        controls = new PlayerInputAction();
    }

    IEnumerator AwakeAsync()
    {
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }

    private void OnEnable()
    {
        controls.Player.Interact.performed += Interact;
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Interact.performed -= Interact;
        controls.Player.Disable();
    }

    /*private void Update()
    {
        if (Input.GetButtonDown("Confirm") && InteractionManager.Instance.currentInteractTarget != null)
        {
            //InteractionManager.Instance.interactTarget.GetComponent<IInteractable>().Interact();
            if (InteractionManager.onInteract != null)
            {
                InteractionManager.onInteract();
            }
        }
    }*/

    public void Interact(InputAction.CallbackContext context)
    {
        if (InteractionManager.onInteract != null)
        {
            InteractionManager.onInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(onCollision!=null)
        {
            onCollision();
        }*/
        if(other.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable newTarget = other.gameObject.GetComponent<Interactable>();
            if (newTarget != null)
            {
                /*int i = 0;
                other.gameObject.GetComponent<IInteractable>().enterPrompt();
                //InteractionManager.Instance.interactTarget = other.gameObject;
                InteractionManager.Instance.IsInRange = true;
                foreach(IInteractable interactable in other.gameObject.GetComponents<IInteractable>())
                {
                    i++;
                    onInteract += interactable.Interact;
                }*/
                InteractionManager.Instance.AddInteractTarget(newTarget);
            }
        }
        /*else
        {
            return;
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        /*if (onCollision != null)
        {
            onCollision();
        }*/
        /*if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            *//*if (InteractionManager.Instance.interactTarget == null)
            {
                InteractionManager.Instance.interactTarget = other.gameObject;
            }*//*
            InteractionManager.Instance.IsInRange = true;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.gameObject.CompareTag("Interactable"))
        {
            int i = 0;
            other.gameObject.GetComponent<IInteractable>().exitPrompt();
            //InteractionManager.Instance.interactTarget = null;
            InteractionManager.Instance.IsInRange = false;
            foreach (IInteractable interactable in other.gameObject.GetComponents<IInteractable>())
            {
                i++;
                *//*onInteract -= interactable.Interact;*//*
            }
        }
        else
        {
            return;
        }*/
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable oldTarget = other.gameObject.GetComponent<Interactable>();
            InteractionManager.Instance.RemoveInteractTarget(oldTarget);
        }
    }

    public void UpdateInventory(Item newItem, int amount)
    {
        /*if(items.Contains(items.Find(x=>x.item==newItem)))
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
        }*/
    }
}
