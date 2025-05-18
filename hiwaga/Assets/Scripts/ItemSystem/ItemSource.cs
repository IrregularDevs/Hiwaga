using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ItemSource : MonoBehaviour, IInteractable
{
    [SerializeField] private string enterString, exitString, interactString;
    [SerializeField] private Item item;
    [SerializeField] private bool hasLimit;
    [SerializeField] private int maxUses;
    public int amountGiven;
    private int uses = 0;

    public void enterPrompt()
    {

    }

    public void exitPrompt()
    {

    }

    public void Interact()
    {
        if (hasLimit == true && uses >= maxUses)
        {
            return;
        }
        else
        {
            if(InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager is missing.");
            }
            InventoryManager.Instance.AddItem(item, this);
            /*if(Player.Instance.quest.Exists(x => x.goal.goalType == GoalType.Fetch))
            {
                Quest quest = Player.Instance.quest.Find(x => x.goal.goalType == GoalType.Fetch);
                quest.goal.ItemFetched();
                if(quest.goal.IsReached())
                {
                    quest.Complete();
                }
            }*/
            return;
        }
    }

    public void ChangeUses(int i)
    {
        uses += i;
    }
}