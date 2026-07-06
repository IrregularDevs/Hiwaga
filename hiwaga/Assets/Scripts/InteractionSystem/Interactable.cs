#nullable enable
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private RawImage? prompt;
    [SerializeField] protected GameStage requiredGameStage;
    [SerializeField] protected GameStage newGameStage;

    public virtual void Interact()
    {

    }

    public virtual void SetActivePrompt(bool state)
    {
        if(prompt != null && GameManager.currentGameStage >= requiredGameStage)
        {
            prompt?.gameObject.SetActive(state);
        }
    }

    private void Awake()
    {
        if (prompt != null)
        {
            prompt?.gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if (prompt != null)
        {
            prompt?.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
    }
}
