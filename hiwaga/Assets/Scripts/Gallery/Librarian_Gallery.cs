using UnityEngine;

public class Librarian_Gallery : NPC
{
    protected override void Start()
    {
        base.Start();
        GameManager.onChangeGameStage -= OnChangeGameState;
        GameManager.onChangeGameStage += OnChangeGameState;
    }

    protected override void ChangeStage()
    {
        switch (GameManager.currentGameStage)
        {
            case GameStage.Tutorial:
                GameManager.ChangeGameStage(GameStage.TutorialFinish);
                break;
            case GameStage.BenitoFinish:
                GameManager.ChangeGameStage(GameStage.Bubuyog);
                break;
            default:
                break;
        }
    }

    private void OnChangeGameState(GameStage newGameStage)
    {
        switch (newGameStage)
        {
            case GameStage.BenitoFinish:
                DialogueManager.Instance.UpdateDialogue(GetRefName(), 2);
                break;
            default:
                break;
        }
    }
}