using UnityEngine;
using System.Collections;
using TMPro;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance => instance;

    [SerializeField] private GameObject questPrefab;
    [SerializeField] private GameObject questList;

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

    public void AddQuest(Quest quest)
    {
        GameObject newQuest = Instantiate(questPrefab, questList.transform);
        newQuest.transform.Find("Title").GetComponent<TMP_Text>().text = quest.title;
        newQuest.transform.Find("Description").GetComponent<TMP_Text>().text = quest.description;
    }
}
