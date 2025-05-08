using TMPro;
using UnityEngine;

public class DebugDisplay : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string currentMessage = "";

    public void SetText(string message)
    {
        currentMessage = message;
        if (debugText != null)
        {
            debugText.text = currentMessage;
        }
    }

    public float clearDelay = 3f;
    private float timer = 0f;

    void Update()
    {
        if (!string.IsNullOrEmpty(currentMessage))
        {
            timer += Time.deltaTime;
            if (timer >= clearDelay)
            {
                debugText.text = "";
                currentMessage = "";
                timer = 0f;
            }
        }
    }
}
