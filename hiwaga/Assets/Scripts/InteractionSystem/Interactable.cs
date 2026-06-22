#nullable enable
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private RawImage? prompt;

    public virtual void Interact()
    {

    }

    public virtual void SetActivePrompt(bool state)
    {
        if(prompt != null)
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

    private void Update()
    {
        if (prompt != null)
        {
            prompt?.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
    }
}
