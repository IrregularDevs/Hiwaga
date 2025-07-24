using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public int count;
    public Item item;
    public Image image;
    public TextMeshProUGUI countText;

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void InitializeItem(Item newItem, int newCount)
    {
        item = newItem;
        image.sprite = newItem.image;
        count = newCount;
        RefreshCount();
    }

    public void UpdateCount(int incomingCount)
    {
        count += incomingCount;
        RefreshCount();
    }

    public void ClearItem()
    {
        item = null;
        image.sprite = null;
        countText.gameObject.SetActive(false);
    }
}
