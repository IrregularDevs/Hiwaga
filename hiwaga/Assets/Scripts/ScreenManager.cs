using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private Slider loadingBarFill;
    [SerializeField] private GameObject loadingScreen;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.value = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Game has been exited.");
        Application.Quit();
    }
}
