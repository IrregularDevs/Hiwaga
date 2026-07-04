using UnityEngine;

public class GameStageProgression : Interactable
{
    public override void Interact()
    {
        GameManager.ChangeGameStage(GameStage.BenitoFinish);
    }
}
