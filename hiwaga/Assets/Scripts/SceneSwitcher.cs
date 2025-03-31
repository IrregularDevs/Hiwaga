using UnityEngine;

public class SceneSwitcher : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneName;
    private ScreenManager screenManager;

    private void Start()
    {
        screenManager = FindFirstObjectByType<ScreenManager>();
    }

    public void Interact()
    {
        screenManager.LoadScene(sceneName);
    }

    public void enterPrompt()
    {
        Debug.Log("SceneSwitcher entered.");
    }

    public void exitPrompt()
    {
        Debug.Log("SceneSwitcher exited.");
    }
}
