using UnityEngine;
using System.Collections;

public class GameInitialization : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(AwakeAsync());
    }

    IEnumerator AwakeAsync()
    {
        InventoryManager.Instance.ShowHidePanel(true);
        QuestManager.Instance.ShowHidePanel(true);
        OptionsManager.Instance.OpenCloseMenu(false);
        yield return null;
    }
}
