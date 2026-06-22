using UnityEngine;

public class Key_Benito : ItemSource
{
    private void Start()
    {
        onInvalidHolder += Vanish;
    }

    private void OnDisable()
    {
        onInvalidHolder -= Vanish;
    }

    private void Vanish()
    {
        InteractionManager.Instance.RemoveInteractTarget(this);
        onInvalidHolder -= Vanish;
        gameObject.SetActive(false);
    }
}
