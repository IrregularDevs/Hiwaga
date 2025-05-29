using UnityEngine;

public class SceneSwitcher : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneName;

    public void Interact()
    {
        ScreenManager.Instance.LoadScene(sceneName);
    }

    public void enterPrompt()
    {
        Debug.Log("SceneSwitcher entered.");
    }

    public void exitPrompt()
    {
        Debug.Log("SceneSwitcher exited.");
    }
    public bool canInteract()
    {
        return true; // Always allow interaction
    }
}
