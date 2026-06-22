using UnityEngine;

public class SceneSwitcher : Interactable
{
    [SerializeField] private string sceneName;

    public override void Interact()
    {
        InteractionManager.Instance.RemoveInteractTarget(this);
        ScreenManager.Instance.LoadScene(sceneName);
    }
}
