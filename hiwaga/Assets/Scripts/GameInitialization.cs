using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    [Tooltip("The scene named \"ScenePersistentInitialization\" must be used. Use this to change which scene the initialization scene goes to next.")]
    [SerializeField] private string sceneName;

    private void Start()
    {
        ScreenManager.Instance.LoadScene(sceneName);
    }
}
