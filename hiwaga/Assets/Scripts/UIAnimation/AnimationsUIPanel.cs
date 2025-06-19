using UnityEngine;
using Unity.UI;

public class AnimationsUIPanel : MonoBehaviour
{
    public Animator canvasUI;
    bool clickedNewGame;
    bool clickedOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasUI = GetComponent<Animator>();
    }

    public void NewGameButton()
    {
        canvasUI.SetBool("clickedNewGame", true);
    }

    public void OptionsButton()
    {
        canvasUI.SetBool("clickedOptions", true);
    }

    public void MainMenuButton()
    {
        canvasUI.SetBool("clickedNewGame", false);
    }

    public void MainMenuButtonO()
    {
        canvasUI.SetBool("clickedOptions", false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
