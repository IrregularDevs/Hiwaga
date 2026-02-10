using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public Image image;
    public Sprite frontSprite;
    public Sprite backSprite;

    bool isFlipped = false;
    MatchGame manager;

    void Start()
    {
        manager = FindObjectOfType<MatchGame>();
        image.sprite = backSprite;
    }

    public void OnClick()
    {
        if (isFlipped || manager.IsBusy) return;

        isFlipped = true;
        image.sprite = frontSprite;
        manager.CardFlipped(this);
    }

    public void FlipBack()
    {
        isFlipped = false;
        image.sprite = backSprite;
    }
}
