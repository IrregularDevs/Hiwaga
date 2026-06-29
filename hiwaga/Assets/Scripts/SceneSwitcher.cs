using UnityEngine;

public class SceneSwitcher : Interactable
{
    [SerializeField] protected string sceneName;

    public override void Interact()
    {
        if(GameManager.currentGameStage >= requiredGameStage)
        {
            GameManager.ChangeGameStage(newGameStage);
            InteractionManager.Instance.RemoveInteractTarget(this);
            ScreenManager.Instance.LoadScene(sceneName);
        }
    }
}
