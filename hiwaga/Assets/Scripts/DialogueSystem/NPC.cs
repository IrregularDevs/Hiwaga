using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPC : Interactable/*, IInteractable*/
{
    //[SerializeField] private DialogueGroup npcDialogue;
    [SerializeField] private int index;
    [SerializeField] private string npcRefName;

    //Delegate
    public delegate void OnBeginDialogue();
    public OnBeginDialogue onBeginDialogue;

    public delegate void OnEndDialogue();
    public OnEndDialogue onEndDialogue;

    protected virtual void Start()
    {
        if(!DialogueManager.Instance)
        {
            Debug.Log("Bruh");
        }
        if (!DialogueManager.Instance.npc_List.Exists(x => x == this))
        {
            Debug.Log("NPC Added.");
            DialogueManager.Instance.AddNPC(this);
        }
        //StartCoroutine(AwakeAsync());
    }

    private void OnEnable()
    {
        onEndDialogue += ChangeStage;
    }

    private void OnDisable()
    {
        InteractionManager.Instance.RemoveInteractTarget(this);
        onEndDialogue -= ChangeStage;
    }

    IEnumerator AwakeAsync()
    {
        yield return null;
    }

    public bool canInteract()
    {
        return !DialogueManager.Instance.isdialogueActive;
    }

    public override void Interact()
    {
        if(GameManager.currentGameStage >= requiredGameStage)
        {
            onBeginDialogue?.Invoke();
            DialogueManager.Instance.BeginDialogue(this);
        }
    }

    public int GetIndex()
    {
        return index;
    }

    public string GetRefName()
    {
        return npcRefName;
    }

    protected virtual void ChangeStage()
    {
        if(GameManager.currentGameStage < newGameStage)
        {
            GameManager.ChangeGameStage(newGameStage);
        }
    }
}
