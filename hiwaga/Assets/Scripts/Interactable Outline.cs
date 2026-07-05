using UnityEngine;

// This script is used to control the outline effect on an interactable object in Unity.
// attach this script to any GameObject that has a Renderer component and an outline shader material applied to it.
//call ShowHighlight() to show the outline and HideHighlight() to hide it.

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