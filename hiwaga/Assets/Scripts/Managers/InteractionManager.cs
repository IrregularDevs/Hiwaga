using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    private static InteractionManager instance;
    public static InteractionManager Instance => instance;

    //public GameObject[] interactTarget;
    public Interactable currentInteractTarget;
    public List<Interactable> interactTargets = new List<Interactable>();
    public bool IsInRange = false;

    [SerializeField] private Sprite interactPrompt;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public delegate void InteractCallback();
    public static InteractCallback onInteract;

    private void Awake()
    {
        instance = this;
    }

    /*IEnumerator AwakeAsync()
    {
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }*/

    public void AddInteractTarget(Interactable newTarget)
    {
        interactTargets.Add(newTarget);
        UpdateInteractTarget();
    }

    public void RemoveInteractTarget(Interactable removedTarget)
    {
        if(interactTargets.Exists(x => x == removedTarget))
        {
            removedTarget.SetActivePrompt(false);
            interactTargets.Remove(removedTarget);
        }
        UpdateInteractTarget();
    }

    public void UpdateInteractTarget()
    {
        if (interactTargets.Count != 0)
        {
            interactTargets[0].SetActivePrompt(true);
            if (currentInteractTarget == null)
            {
                currentInteractTarget = interactTargets[0];
            }
            if (onInteract != interactTargets[0].Interact)
            {
                onInteract = interactTargets[0].Interact;
            }
        }
        else
        {
            currentInteractTarget = null;
            onInteract = null;
        }
    }
}
