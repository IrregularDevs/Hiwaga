using UnityEngine;

public class Princess_Benito : NPC
{
    [SerializeField] private Gate_Benito gate;

    private void Start()
    {
        onEndDialogue += ChangeIndex;
        onEndDialogue += gate.SwitchScene;
    }

    private void OnDisable()
    {
        onEndDialogue -= ChangeIndex;
        onEndDialogue -= gate.SwitchScene;
    }

    private void ChangeIndex()
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
        PlayerPrefs.SetInt("Librarian_Testing", 2);
        PlayerPrefs.Save();
    }
}
