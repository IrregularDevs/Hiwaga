using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    [SerializeField] private Animator cutsceneAnimator;
    private Princess_Benito princess;

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
        cutsceneAnimator.gameObject.SetActive(false);
    }

    public void PlayPrincessCutscene(Princess_Benito newPrincess)
    {
        cutsceneAnimator.gameObject.SetActive(true);
        princess = newPrincess;
        cutsceneAnimator.Play("Test_Cutscene");
    }

    public void EndPrincessCutscene()
    {
        Debug.Log("Wallah");
        cutsceneAnimator.gameObject.SetActive(false);
        Debug.Log("EUIOPQ");
        princess.StartGate();
    }
}
