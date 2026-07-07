#nullable enable
using UnityEngine;

public class Bird_Benito : NPC
{
    [SerializeField] private NPC_ModelChanger? modelChanger;
    [SerializeField] private Gate_Benito? gate;
    [SerializeField] private bool hasTalked = false;
    [SerializeField] private bool inHeaven = false;
    [SerializeField] private bool inHell = false;
    private bool canLeave = false;

    protected override void Start()
    {
        base.Start();
        onBeginDialogue += ChangeModel;
        if(inHeaven)
        {
            onEndDialogue -= EndHeaven;
            onEndDialogue += EndHeaven;
        }
        if(inHell)
        {
            onEndDialogue -= EndHell;
            onEndDialogue += EndHell;
        }
    }

    private void OnDisable()
    {
        onBeginDialogue -= ChangeModel;
        if (inHeaven)
        {
            onEndDialogue -= EndHeaven;
        }
        if (inHell)
        {
            onEndDialogue -= EndHell;
        }
    }

    private void ChangeModel()
    {
        modelChanger?.ChangeModel(1);
        onBeginDialogue -= ChangeModel;
    }

    public override void Interact()
    {
        if(!hasTalked && !inHeaven && !inHell)
        {
            DialogueManager.Instance.UpdateDialogue(GetRefName(), 1);
            hasTalked = true;
        }
        base.Interact();
    }

    private void EndHeaven()
    {
        if(canLeave)
        {
            gate?.SwitchScene();
        }
    }

    private void EndHell()
    {
        if (canLeave)
        {
            gate?.SwitchScene();
        }
    }

    public void ProgressStory(bool state)
    {
        canLeave = state;
    }
}
