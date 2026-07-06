using UnityEngine;

public class Gate_Benito : Lock
{
    [SerializeField] private string sceneName;

    protected virtual void Start()
    {
        onInteract += SwitchScene;
    }

    private void OnDisable()
    {
        onInteract -= SwitchScene;
    }

    public void SwitchScene()
    {
        InteractionManager.Instance.RemoveInteractTarget(this);
        ScreenManager.Instance.LoadScene(sceneName);
    }
}
