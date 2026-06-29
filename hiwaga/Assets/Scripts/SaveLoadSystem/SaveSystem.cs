using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public const string FILENAME_SAVEDATA = "/savadata.json";

    public static void SaveGameState()
    {
        string filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
        DialogueIndexData dialogueIndexData = new DialogueIndexData();
        SceneData sceneData = new SceneData();
        PlayerData playerData = new PlayerData();
        SaveData saveData = new SaveData(dialogueIndexData, sceneData, playerData);
        string txt = JsonUtility.ToJson(saveData);
        File.WriteAllText(FILENAME_SAVEDATA, txt);
    }

    public static void EraseSaveData()
    {
        string filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
        if(File.Exists(filePathSaveData))
        {
            File.Delete(filePathSaveData);
        }
    }
}

[System.Serializable]
public class SaveData
{
    [SerializeField] public DialogueIndexData dialogueIndexData;
    [SerializeField] public SceneData sceneData;
    [SerializeField] public PlayerData playerData;

    public SaveData(DialogueIndexData dialogueIndexData, SceneData sceneData, PlayerData playerData)
    {
        this.dialogueIndexData = dialogueIndexData;
        this.sceneData = sceneData;
        this.playerData = playerData;
    }

    public SaveData(SceneData sceneData)
    {
        this.sceneData = sceneData;
    }

    public SaveData(PlayerData playerData)
    {
        this.playerData = playerData;
    }
}

[System.Serializable]
public class DialogueIndexData
{
    /*[SerializeField] public List<string> npcNames;
    [SerializeField] public List<int> npcDialogueIndex;*/
    [SerializeField] public Dictionary<string, int> m_dialogueIndex = new Dictionary<string, int>();

    public DialogueIndexData()
    {
        /*foreach(NPC npc in DialogueManager.Instance.npc_List)
        {
            npcNames = dialogueManager.npc_List.GetRefName;
        }*/
        //npcNames = DialogueManager.Instance.npc_List.ConvertAll(new System.Converter<NPC, string>(NPCToString));
        if(DialogueManager.Instance != null)
        {
            foreach (KeyValuePair<string, int> kvp in DialogueManager.Instance.m_npcDictionary)
            {
                m_dialogueIndex[kvp.Key] = kvp.Value;
            }
        }
    }

    public void ChangeIndex(string npcName, int i)
    {

    }

    /*public string NPCToString(NPC npc)
    {
        return npc.GetRefName();
    }*/
}

[System.Serializable]
public class SceneData
{
    [SerializeField] public string scene;

    public SceneData()
    {
        scene = SceneManager.GetActiveScene().name;
    }
}

[System.Serializable]
public class PlayerData
{
    [SerializeField] public string item;
    [SerializeField] public Vector3 location;

    public PlayerData()
    {
        if(Player.Instance != null && Player.Instance.GetCurrentItem() != null)
        {
            item = Player.Instance.GetCurrentItem().itemName;
        }
        else
        {
            item = null;
        }
        location = Player.Instance.gameObject.transform.position;
    }
}

[System.Serializable]
public class VolumeData
{

}