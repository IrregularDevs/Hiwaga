using UnityEngine;

public class Princess_Benito : NPC
{
    [SerializeField] private Gate_Benito gate;
    private bool hasInteracted = false;

    protected override void Start()
    {
        base.Start();
        onEndDialogue -= ChangeIndex;
        onEndDialogue += ChangeIndex;
    }

    private void OnDisable()
    {
        onEndDialogue -= ChangeIndex;
    }

    private void ChangeIndex()
    {
        /*SaveData saveData = LoadSystem.LoadGameData();
        if (saveData != null)
        {
            saveData.dialogueIndexData.ChangeIndex("Librarian_Testing", 2);
        }
        else
        {
            SaveSystem.SaveGameState();
        }*/
        /*PlayerPrefs.SetInt("Librarian_Testing", 2);
        PlayerPrefs.Save();*/
        //DialogueManager.Instance.UpdateDialogue("Librarian_Testing", 2);
        if (GameManager.currentGameStage <= GameStage.Benito)
        {
            hasInteracted = true;
            Debug.Log("Pricness Benito called");
            CutsceneManager.Instance.PlayPrincessCutscene(this);
        }
    }

    public void StartGate()
    {
        InteractionManager.Instance.RemoveInteractTarget(this);
        gate.SwitchScene();
    }

    public override void Interact()
    {
        if(!hasInteracted)
        {
            base.Interact();
        }
    }
}
