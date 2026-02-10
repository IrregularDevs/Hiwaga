using UnityEngine;
using System.Collections;
using TMPro;

public class MatchGame : MonoBehaviour
{
    public TMP_Text message;

    Card firstCard;
    Card secondCard;

    public bool IsBusy { get; private set; }

    public void CardFlipped(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        IsBusy = true;
        yield return new WaitForSeconds(1f);

        if (firstCard.cardID == secondCard.cardID)
        {
            message.text = "Great job! 🎉";
        }
        else
        {
            firstCard.FlipBack();
            secondCard.FlipBack();
            message.text = "Try again 🙂";
        }

        firstCard = null;
        secondCard = null;
        IsBusy = false;
    }
}
