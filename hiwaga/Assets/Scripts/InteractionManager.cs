using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private GameObject interactTarget;
    private bool IsInRange = false;
    [SerializeField]
    private GameObject dialoguePanel;

    private void Update()
    {
        if (Input.GetButtonDown("Confirm") && interactTarget && IsInRange)
        {
            dialoguePanel.SetActive(true);
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            other.gameObject.GetComponent<Interactable>().enterPrompt();
            interactTarget = other.gameObject;
            IsInRange = true;
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
            other.gameObject.GetComponent<Interactable>().exitPrompt();
            interactTarget = null;
            IsInRange = false;
        }
    }

    private void Interact()
    {
        interactTarget.GetComponent<Interactable>().interactPrompt();
    }
}
