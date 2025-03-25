using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void enterPrompt();
    public void exitPrompt();
}

public class InteractionManager : MonoBehaviour
{
    private GameObject interactTarget;
    private bool IsInRange = false;

    private void Update()
    {
        if (Input.GetButtonDown("Confirm") && interactTarget && IsInRange)
        {
            interactTarget.GetComponent<IInteractable>().Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            other.gameObject.GetComponent<IInteractable>().enterPrompt();
            interactTarget = other.gameObject;
            IsInRange = true;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if(interactTarget == null)
            {
                interactTarget = other.gameObject;
            }
            IsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            other.gameObject.GetComponent<IInteractable>().exitPrompt();
            interactTarget = null;
            IsInRange = false;
        }
        else
        {
            return;
        }
    }
}
