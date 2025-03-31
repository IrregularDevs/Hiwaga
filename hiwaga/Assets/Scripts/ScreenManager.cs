using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if(sceneName!=null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadScene()
    {
        Debug.Log("Scene does not exist.");
    }

    public void ExitGame()
    {
        Debug.Log("Game has been exited.");
        Application.Quit();
    }
}
