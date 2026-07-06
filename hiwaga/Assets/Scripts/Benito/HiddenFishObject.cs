using UnityEngine;
using System.Collections;
using TMPro;

public class HiddenFishObject : Interactable
{
    [Header("Set in Inspector")]
    public bool isCorrectFish = false;

    [Header("Fish Reference")]
    public GameObject fishModel;
    public FishServant_Benito fishServant;

    [Header("Popup Reference")]
    public GameObject popup;

    [Header("Text Animator Reference")]
    public TextMeshProUGUI popup_Text;

    private bool alreadyInteracted = false;

    [SerializeField] private float duration;

    private void Start()
    {
        /*popup_Animator.Play("Sea_Stable");
        popup_Animator.SetBool("Stable?", true);*/
        popup_Text.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        //if (alreadyInteracted) return;

        alreadyInteracted = true;

        if (isCorrectFish)
        {
            RevealFish();
        }
        else
        {
            StartCoroutine(WrongChoice());
        }
    }

    void RevealFish()
    {

        // show model if you still use a separate visual object
        if (fishModel != null)
            fishModel.SetActive(true);

        // activate NPC fish
        if (fishServant != null)
            fishServant.RevealFish();
    }

    private IEnumerator WrongChoice()
    {
        float currentTime = 0f;

        popup_Text.gameObject.SetActive(true);
        Color textColor = popup_Text.color;
        textColor.a = 0f;
        popup_Text.color = textColor;

        while(currentTime < duration)
        {
            textColor.a = currentTime / duration;
            popup_Text.color = textColor;
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0f;

        while (currentTime < duration)
        {
            textColor.a = 1 - (currentTime / duration);
            popup_Text.color = textColor;
            currentTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 0f;
        popup_Text.color = textColor;

        /*popup.SetActive(true);
        Vector3 startPos = popup.transform.position;
        Vector3 targetPos = startPos + Vector3.up * moveDistance;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            float percent = elapsedTime / duration;
            popup.transform.position = Vector3.Lerp(startPos, targetPos, percent);
            elapsedTime += Time.deltaTime;
            
        }
        //popup_Animator.SetBool("Stable?", false);
        popup.transform.position = targetPos;
        popup.SetActive(false);*/
    }

    protected override void Update()
    {
        base.Update();
        Vector3 playerLoc = Player.Instance.transform.position;
        //Vector3 mainCamPos = Camera.main.transform.position;
        popup.transform.position = new Vector3(playerLoc.x, playerLoc.y + 3f, playerLoc.z);
        popup.transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }

}