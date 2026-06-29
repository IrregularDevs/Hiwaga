using UnityEngine;

public class OldWoman_Benito : NPC
{
    [SerializeField] private ItemSource itemSource;

    protected override void Start()
    {
        base.Start();
        onBeginDialogue += GiveSword;
    }

    private void OnDisable()
    {
        onBeginDialogue -= GiveSword;
    }

    private void GiveSword()
    {
        itemSource.Interact();
        onBeginDialogue -= GiveSword;
    }
}
