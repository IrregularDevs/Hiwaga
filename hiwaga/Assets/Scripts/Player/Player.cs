using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Confirm") && InteractionManager.Instance.interactTarget && InteractionManager.Instance.IsInRange)
        {
            InteractionManager.Instance.interactTarget.GetComponent<IInteractable>().Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            other.gameObject.GetComponent<IInteractable>().enterPrompt();
            InteractionManager.Instance.interactTarget = other.gameObject;
            InteractionManager.Instance.IsInRange = true;
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
            if (InteractionManager.Instance.interactTarget == null)
            {
                InteractionManager.Instance.interactTarget = other.gameObject;
            }
            InteractionManager.Instance.IsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            other.gameObject.GetComponent<IInteractable>().exitPrompt();
            InteractionManager.Instance.interactTarget = null;
            InteractionManager.Instance.IsInRange = false;
        }
        else
        {
            return;
        }
    }

}
