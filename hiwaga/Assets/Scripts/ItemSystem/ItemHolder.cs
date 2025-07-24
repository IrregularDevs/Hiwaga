using UnityEngine;

[System.Serializable]
public class ItemHeld
{
    public Item item;
    public int count;
}

public class ItemHolder : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemHeld[] itemsHeld;
    [SerializeField] protected bool hasLimit;
    [SerializeField] protected int maxUses;
    [SerializeField] protected int uses;
    [SerializeField] protected bool disappears;

    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }

    public bool canInteract()
    {
        return true;
    }

    public void ChangeUses(int i)
    {
        uses += i;
    }

    public virtual void Interact()
    {

    }

    public int GetUses()
    {
        return uses;
    }

    public ItemHeld[] GetItems()
    {
        return itemsHeld;
    }

    public void DisappearCheck()
    {
        if (hasLimit == true && uses >= maxUses && disappears)
        {
            Player.onInteract -= Interact;
            gameObject.SetActive(false);
        }
    }
}
