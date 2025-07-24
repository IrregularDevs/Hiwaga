using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    private static InteractionManager instance;
    public static InteractionManager Instance => instance;

    public static IInteractable[] interactTarget;
    public static bool IsInRange = false;

    public UnityEvent OnInteract;

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }
}
