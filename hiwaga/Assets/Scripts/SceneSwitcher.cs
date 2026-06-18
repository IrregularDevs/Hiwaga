using UnityEngine;

public class SceneSwitcher : Interactable
{
    [SerializeField] private string sceneName;

    public override void Interact()
    {
        ScreenManager.Instance.LoadScene(sceneName);
    }
}
