using UnityEngine;

[System.Serializable]
public enum GameState
{
    MainMenu = 0,
    LoadingScreen = 1,
    Pause = 2,
    Dialogue = 3,
    Overworld = 4
}

[System.Serializable]
public enum GameStage
{
    Tutorial = 0,
    TutorialFinish = 1,
    Benito = 2,
    BenitoFinish = 3,
    Bubuyog = 4,
    BubuyogFinish = 5
}

public static class GameManager
{
    public static GameState currentGameState;
    public static GameStage currentGameStage;

    public delegate void OnChangeGameState(GameState newGameState);
    public delegate void OnChangeGameStage(GameStage newGameStage);

    public static OnChangeGameState onChangeGameState;
    public static OnChangeGameStage onChangeGameStage;

    public static void OnApplicationQuit()
    {
        SaveSystem.SaveGameState();
    }

    public static void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SaveSystem.EraseSaveData();
        currentGameStage = GameStage.Tutorial;
        currentGameState = GameState.Overworld;
    }

    public static void ChangeGameStage(GameStage newGameStage)
    {
        currentGameStage = newGameStage;

        switch(currentGameStage)
        {
            case GameStage.Tutorial:
                break;
            case GameStage.Benito:
                break;
            case GameStage.Bubuyog:
                break;
        }
        onChangeGameStage?.Invoke(currentGameStage);
    }

    public static void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;

        switch (currentGameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Pause:
                break;
            case GameState.Dialogue:
                break;
            case GameState.Overworld:
                break;
        }
        onChangeGameState?.Invoke(currentGameState);
    }
}