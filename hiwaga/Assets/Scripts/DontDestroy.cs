using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("GameObject will not be destroyed.");
        DontDestroyOnLoad(gameObject);
    }
}
