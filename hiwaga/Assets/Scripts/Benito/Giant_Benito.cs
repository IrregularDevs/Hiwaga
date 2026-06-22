#nullable enable
using UnityEngine;

public class Giant_Benito : Lock
{
    //[SerializeField] private GameObject? giant;
    [SerializeField] private GameObject? door;
    [SerializeField] private ItemSource? itemSource;

    private void Start()
    {
        gameObject.SetActive(true);
        if(door != null)
        {
            door?.SetActive(true);
        }
        if(itemSource != null)
        {
            itemSource?.gameObject.SetActive(false);
        }
        onInteract += SlayGiant;
    }

    private void OnDisable()
    {
        onInteract -= SlayGiant;
        InteractionManager.Instance.RemoveInteractTarget(this);
    }

    private void SlayGiant()
    {
        if (door != null)
        {
            door?.SetActive(false);
        }
        if (itemSource != null)
        {
            itemSource?.gameObject.SetActive(true);
        }
        InteractionManager.Instance.RemoveInteractTarget(this);
        onInteract -= SlayGiant;
        gameObject.SetActive(false);
    }
}