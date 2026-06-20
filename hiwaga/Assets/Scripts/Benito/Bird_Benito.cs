using UnityEngine;

public class Bird_Benito : NPC
{
    [SerializeField] private NPC_ModelChanger modelChanger;

    private void Start()
    {
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
