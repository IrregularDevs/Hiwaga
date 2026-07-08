using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    [SerializeField] private Animator princessCutsceneAnimator;
    [SerializeField] private Animator fishCutsceneAnimator;
    [SerializeField] private Animator galleryCutsceneAnimator;
    [SerializeField] private Animator forestCutsceneAnimator;
    [SerializeField] private Animator finalCutsceneAnimator;
    private Princess_Benito princess;
    private FishServant_Benito fish, rat;
    private Bird_Benito bird;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        princessCutsceneAnimator.gameObject.SetActive(false);
        fishCutsceneAnimator.gameObject.SetActive(false);
        galleryCutsceneAnimator.gameObject.SetActive(false);
        forestCutsceneAnimator.gameObject.SetActive(false);
    }

    public void PlayPrincessCutscene(Princess_Benito newPrincess)
    {
        princessCutsceneAnimator.gameObject.SetActive(true);
        princess = newPrincess;
        princessCutsceneAnimator.Play("Test_Cutscene");
    }

    public void EndPrincessCutscene()
    {
        princessCutsceneAnimator.gameObject.SetActive(false);
        princess.StartGate();
    }

    public void PlayFishCutscene(FishServant_Benito newFish)
    {
        fishCutsceneAnimator.gameObject.SetActive(true);
        fish = newFish;
        fishCutsceneAnimator.Play("Test_Cutscene3");
    }

    public void EndFishCutscene()
    {
        fishCutsceneAnimator.gameObject.SetActive(false);
        fish.SwitchScene();
    }

    public void PlayGalleryCutscene()
    {
        galleryCutsceneAnimator.gameObject.SetActive(true);
        galleryCutsceneAnimator.Play("GalleryCutscene_Test");
    }

    public void EndGalleryCutscene()
    {
        galleryCutsceneAnimator.gameObject.SetActive(false);
        ScreenManager.Instance.NewGame("Gallery_NEW");
    }

    public void PlayForestCutscene(FishServant_Benito newRat)
    {
        forestCutsceneAnimator.gameObject.SetActive(true);
        rat = newRat;
        forestCutsceneAnimator.Play("ForestCutscene");
    }

    public void EndForestCutscene()
    {
        forestCutsceneAnimator.gameObject.SetActive(false);
        rat.SwitchScene();
    }

    public void PlayFinalCutscene(Bird_Benito newBird)
    {
        finalCutsceneAnimator.gameObject.SetActive(true);
        bird = newBird;
        finalCutsceneAnimator.Play("FinalCutscene");
    }

    public void EndFinalCutscene()
    {
        DialogueManager.Instance.UpdateDialogue("Librarian_Testing", 2);
        finalCutsceneAnimator.gameObject.SetActive(false);
        bird.OnGate();
    }
}
