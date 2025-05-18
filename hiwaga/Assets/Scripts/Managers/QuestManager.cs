using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance => instance;

    [SerializeField] private GameObject questPrefab;
    [SerializeField] private GameObject questTracker;
    [SerializeField] private List<GameObject> quests = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        yield return null;
    }

    public bool FindQuest(string title)
    {
        return quests.Exists(x => x.transform.Find("Title").GetComponent<TMP_Text>().text == title);
    }

    public void AddQuest(Quest quest)
    {
        if(FindQuest(quest.title))
        {
            return;
        }
        else
        {
            GameObject newQuest = Instantiate(questPrefab, questTracker.transform);
            newQuest.transform.Find("Title").GetComponent<TMP_Text>().text = quest.title;
            newQuest.transform.Find("Description").GetComponent<TMP_Text>().text = quest.description;
            quests.Add(newQuest);
            Player.Instance.quests.Add(quest);
            Player.onInventoryUpdate += quest.onFetch;
        }
    }
}
