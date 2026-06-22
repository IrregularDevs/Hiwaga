using UnityEngine;

public class Gate_Benito : Lock
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        onInteract = SwitchScene;
    }

    private void SwitchScene()
    {
        ScreenManager.Instance.LoadScene(sceneName);
    }
}
