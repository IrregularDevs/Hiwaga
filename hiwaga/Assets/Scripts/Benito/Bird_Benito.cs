using UnityEngine;

public class Bird_Benito : NPC
{
    [SerializeField] private NPC_ModelChanger modelChanger;

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
}
