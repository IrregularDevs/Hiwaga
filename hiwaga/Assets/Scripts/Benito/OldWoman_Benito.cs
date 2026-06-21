using UnityEngine;

public class OldWoman_Benito : NPC
{
    [SerializeField] private ItemSource itemSource;

    private void Start()
    {
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
