using UnityEngine;

public class Bird_Benito : NPC
{
    [SerializeField] private NPC_ModelChanger modelChanger;
    private bool hasTalked = false;

    protected override void Start()
    {
        base.Start();
        onBeginDialogue += ChangeModel;
    }

    private void OnDisable()
    {
        onBeginDialogue -= ChangeModel;
    }

    private void ChangeModel()
    {
        modelChanger.ChangeModel(1);
        onBeginDialogue -= ChangeModel;
    }

    public override void Interact()
    {
        if(!hasTalked)
        {
            DialogueManager.Instance.UpdateDialogue(GetRefName(), 1);
            hasTalked = true;
        }
        base.Interact();
    }
}
