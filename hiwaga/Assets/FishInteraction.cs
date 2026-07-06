using UnityEngine;

public class FishServant_Benito : Gate_Benito
{
    private bool hasTalked = false;
    private bool isHidden = true;

    protected override void Start()
    {
        gameObject.SetActive(false); // hidden at start
    }

    public void RevealFish()
    {
        isHidden = false;
        gameObject.SetActive(true);

        Debug.Log("FishServant_Benito revealed!");
    }

    public override void Interact()
    {
        if (isHidden)
        {
            Debug.Log("Fish is still hidden...");
            return;
        }

        if (!hasTalked)
        {
            CutsceneManager.Instance.PlayFishCutscene(this);
            /*DialogueManager.Instance.UpdateDialogue(GetRefName(), 1);
            hasTalked = true;*/
        }

        base.Interact();
    }
}