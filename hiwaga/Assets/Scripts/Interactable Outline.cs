using UnityEngine;

public class InteractableOutline : MonoBehaviour
{
    [SerializeField] Color highlightColor = Color.yellow;
    Color defaultColor = Color.black;

    Renderer rend;
    MaterialPropertyBlock mpb;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    public void ShowHighlight()
    {
        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_Outline_Color", highlightColor);
        rend.SetPropertyBlock(mpb);
    }

    public void HideHighlight()
    {
        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_Outline_Color", defaultColor);
        rend.SetPropertyBlock(mpb);
    }
}


// example of how to use this script with a trigger collider to automatically show and hide the outline when the player enters or exits the trigger area.
/** void OnTriggerEnter(Collider other)
{
    other.GetComponent<InteractableOutline>()?.ShowHighlight();
}

void OnTriggerExit(Collider other)
{
    other.GetComponent<InteractableOutline>()?.HideHighlight();
}
**/