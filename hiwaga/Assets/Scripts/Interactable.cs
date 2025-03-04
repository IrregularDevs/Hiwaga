using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string enterString, exitString, interactString;

    public void enterPrompt()
    {
        Debug.Log(enterString);
    }

    public void exitPrompt()
    {
        Debug.Log(exitString);
    }

    public void interactPrompt()
    {
        Debug.Log(interactString);
    }
}
