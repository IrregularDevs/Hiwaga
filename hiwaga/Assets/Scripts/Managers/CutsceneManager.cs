using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    [SerializeField] private Animator princessCutsceneAnimator;
    [SerializeField] private Animator fishCutsceneAnimator;
    private Princess_Benito princess;
    private FishServant_Benito fish;

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
    }

    public void PlayPrincessCutscene(Princess_Benito newPrincess)
    {
        princessCutsceneAnimator.gameObject.SetActive(true);
        princess = newPrincess;
        princessCutsceneAnimator.Play("Test_Cutscene");
    }

    public void EndPrincessCutscene()
    {
        Debug.Log("Wallah");
        princessCutsceneAnimator.gameObject.SetActive(false);
        Debug.Log("EUIOPQ");
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
        Debug.Log("er");
        fishCutsceneAnimator.gameObject.SetActive(false);
        Debug.Log("th");
        fish.SwitchScene();
    }
}
