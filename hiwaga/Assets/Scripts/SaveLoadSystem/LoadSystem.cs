using UnityEngine;
using System.IO;

public static class LoadSystem
{
    public static SaveData LoadGameData()
    {
        try
        {
            string filePath = Application.persistentDataPath + SaveSystem.FILENAME_SAVEDATA;
            string fileContent = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(fileContent);
            return saveData;
        }
        catch
        {
            return null;
        }
    }
}
