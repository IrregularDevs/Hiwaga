using UnityEngine;

public class Princess_Benito : NPC
{
    [SerializeField] private Gate_Benito gate;

    protected override void Start()
    {
        base.Start();
        GameManager.onChangeGameStage += ChangeIndex;
    }

    private void OnDisable()
    {
        GameManager.onChangeGameStage -= ChangeIndex;
    }

    private void ChangeIndex(GameStage gameStage)
    {
        /*SaveData saveData = LoadSystem.LoadGameData();
        if (saveData != null)
        {
            saveData.dialogueIndexData.ChangeIndex("Librarian_Testing", 2);
        }
        else
        {
            SaveSystem.SaveGameState();
        }*/
        /*PlayerPrefs.SetInt("Librarian_Testing", 2);
        PlayerPrefs.Save();*/
        //DialogueManager.Instance.UpdateDialogue("Librarian_Testing", 2);
        if (GameManager.currentGameStage < GameStage.BenitoFinish)
        {
            Debug.Log("Pricness Benito called");
            GameManager.ChangeGameStage(GameStage.BenitoFinish);
        }
        gate.SwitchScene();
    }
}
