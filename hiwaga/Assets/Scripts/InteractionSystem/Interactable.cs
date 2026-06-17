using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private RawImage prompt;

    public virtual void Interact()
    {

    }

    public void SetActivePrompt(bool state)
    {
        prompt.gameObject.SetActive(state);
    }

    private void Awake()
    {
        prompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        prompt.transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
