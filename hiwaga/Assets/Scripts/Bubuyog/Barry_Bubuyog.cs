using UnityEngine;

public class Barry_Bubuyog : NPC
{
    [SerializeField] private Lock barryLock;
    private bool isCollecting = false;

    public override void Interact()
    {
        if(!isCollecting)
        {
            base.Interact();
        }
        else
        {

        }
    }
}
