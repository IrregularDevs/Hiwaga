using UnityEngine;

public enum GameState
{
    MainMenu,
    Pause,
    Dialogue,
    Overworld
}

public static class GameManager
{
    public static void OnApplicationQuit()
    {
        SaveSystem.SaveGameState();
    }

    public static void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SaveSystem.EraseSaveData();
    }
}