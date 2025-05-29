using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void enterPrompt();
    public void exitPrompt();
    public bool canInteract();
}
