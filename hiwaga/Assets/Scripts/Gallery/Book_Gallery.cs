using UnityEngine;

public class Book_Gallery : SceneSwitcher
{
    private void Update()
    {
        if(GameManager.currentGameStage >= requiredGameStage)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
