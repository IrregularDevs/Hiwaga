using UnityEngine;

public class NPC_ModelChanger : MonoBehaviour
{
    private int currentModelIndex = 0;
    [SerializeField] private GameObject[] models;

    public void ChangeModel(int newModel)
    {
        if(newModel < 0 || newModel >= models.Length)
        {
            return;
        }
        models[currentModelIndex].SetActive(false);
        currentModelIndex = newModel;
        models[currentModelIndex].SetActive(true);
    }
}
