using UnityEngine;
using UnityEngine.UI;

public class AnimationsUI : MonoBehaviour
{
    public Animator cameraAnimator;
    bool clickedNewGame;
    bool clickedOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
    }

    public void NewGameButton()
    {
        cameraAnimator.SetBool("clickedNewGame", true);
    }

    public void OptionsButton()
    {
        cameraAnimator.SetBool("clickedOptions", true);
    }

    public void MainMenuButton()
    {
        cameraAnimator.SetBool("clickedNewGame", false);
    }

    public void MainMenuButtonO()
    {
        cameraAnimator.SetBool("clickedOptions", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
