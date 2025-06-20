using UnityEngine;
using UnityEngine.UI;

public class AnimationsUI : MonoBehaviour
{
    public Animator cameraAnimator;
    public Animator canvasUI;
    bool clickedNewGame;
    bool clickedOptions;


    public void NewGameButton()
    {
        cameraAnimator.SetBool("clickedNewGame", true);
        canvasUI.SetBool("clickedNewGame", true);
    }

    public void OptionsButton()
    {
        cameraAnimator.SetBool("clickedOptions", true);
        canvasUI.SetBool("clickedOptions", true);
    }

    public void MainMenuButton()
    {
        cameraAnimator.SetBool("clickedNewGame", false);
        canvasUI.SetBool("clickedNewGame", false);
    }

    public void MainMenuButtonO()
    {
        cameraAnimator.SetBool("clickedOptions", false);
        canvasUI.SetBool("clickedOptions", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
